import pytest
import requests
from datetime import datetime


@pytest.fixture
def _url():
    return "http://localhost:5000/api/supplier"


headers = {
    "Accept": "/",
    "User-Agent": "value",
    "API_key": "a1b2c3d4e5",  #  the API key
}


def test_add_remove_suppliers(_url):

    new_supplier = {
        "Id": 99999999,
        "Code": "SUP0001",
        "Name": "testsupplier",
        "Address": "unknown",
        "AddressExtra": "Apt. 420",
        "City": "Port Anitaburgh",
        "ZipCode": "91688",
        "Province": "Illinois",
        "Country": "Czech Republic",
        "ContactName": "Toni Barnett",
        "PhoneNumber": "363.541.7282x36825",
        "Reference": "LPaJ-SUP0001",
    }

    post_response = requests.post(_url, json=new_supplier, headers=headers)
    assert post_response.status_code == 201

    get_response = requests.get(
        _url + f"/{post_response.json()['id']}", headers=headers
    )

    if get_response.content:
        response_data = get_response.json()
        assert response_data["name"] == "testsupplier"
    else:
        print("GET request returned 200 but no body")

    del_response = requests.delete(
        _url + f"/{post_response.json()['id']}", headers=headers
    )
    assert del_response.status_code == 204


def test_put_suppliers(_url):
    new_supplier = {
        "Id": 99999999,
        "Code": "SUP0001",
        "Name": "testsupplier",
        "Address": "unknown",
        "AddressExtra": "Apt. 420",
        "City": "Port Anitaburgh",
        "ZipCode": "91688",
        "Province": "Illinois",
        "Country": "Czech Republic",
        "ContactName": "Toni Barnett",
        "PhoneNumber": "363.541.7282x36825",
        "Reference": "LPaJ-SUP0001",
    }
    post_response = requests.post(_url, json=new_supplier, headers=headers)
    id = post_response.json()["id"]

    updated_supplier = {
        "Code": "SUP0001",
        "Name": "testsupplier",
        "Address": "Hofplein",
        "AddressExtra": "Apt. 420",
        "City": "Port Anitaburgh",
        "ZipCode": "91688",
        "Province": "Illinois",
        "Country": "Czech Republic",
        "ContactName": "Toni Barnett",
        "PhoneNumber": "363.541.7282x36825",
        "Reference": "LPaJ-SUP0001",
    }
    put_response = requests.put(_url + f"/{id}", json=updated_supplier, headers=headers)
    assert put_response.status_code == 200

    if put_response.content:
        response_data = put_response.json()  # Parse JSON response if body is not empty
        assert response_data["address"] == "Hofplein"
    else:
        print("PUT request returned 200 but no response body.")

    requests.delete(_url + f"/{id}", headers=headers)


def test_url_suppliers(_url):
    get_response = requests.get(_url + "/url_part2" + "/url_part3", headers=headers)
    assert get_response.status_code == 404


def test_input_suppliers(_url):
    get_response = requests.get(_url + "/seven", headers=headers)
    assert get_response.status_code == 400


def test_url_specification_suppliers(_url):
    get_response = requests.get(
        _url + "/1" + "/utems", headers=headers
    )  # a misspelling for items
    assert get_response.status_code == 404


def test_time_suppliers(_url):
    requests.delete(_url + "/999999999", headers=headers)
    requests.delete(_url + "/999999999", headers=headers)

    new_supplier = {
        "id": 999999999,
        "name": "testsupplier",
        "address": "test_suppliers.py",
    }
    requests.post(_url, json=new_supplier, headers=headers)

    get_result = requests.get(_url + "/999999999", headers=headers)
    if get_result.content:
        response_data = get_result.json()  # Parse JSON response if body is not empty
        assert (
            response_data["CreatedAt"].split(":")[0]
            == datetime.utcnow().isoformat().split(":")[0]
        )
    else:
        print("get request returned 200 but no response body.")

    requests.delete(_url + "/999999999", headers=headers)
    requests.delete(_url + "/999999999", headers=headers)
