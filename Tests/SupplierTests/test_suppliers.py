import pytest
import requests
from datetime import datetime


@pytest.fixture
def _url():
    return "http://localhost:3000/api/v1/suppliers"


headers = {
    "Accept": "/",
    "User-Agent": "value",
    "API_KEY": "a1b2c3d4e5",  #  the API key
}


def test_add_remove_suppliers(_url):

    new_supplier = {
        "id": 99999999,
        "name": "testsupplier",
        "address": "test_suppliers.py",
    }

    post_response = requests.post(_url, json=new_supplier, headers=headers)
    assert post_response.status_code == 201

    get_response = requests.get(_url + "/99999999", headers=headers)

    if get_response.content:
        response_data = get_response.json()
        assert response_data["name"] == "testsupplier"
    else:
        print("GET request returned 200 but no body")

    del_response = requests.delete(_url + "/99999999", headers=headers)
    assert del_response.status_code == 200

    get_response2 = requests.get(_url + "/99999999", headers=headers)
    assert get_response2.status_code == 200
    if get_response2.content:
        response_data = get_response2.json()  # Parse JSON response if body is not empty
        assert response_data == None
    else:
        print("GET request returned 200 but no response body.")


def test_put_suppliers(_url):
    new_supplier = {
        "id": 999999999,
        "name": "testsupplier",
        "address": "test_suppliers.py",
    }
    requests.post(_url, json=new_supplier, headers=headers)

    updated_supplier = {
        "id": 999999999,
        "name": "testsupplier updated",
        "address": "test_suppliers.py",
    }
    put_response = requests.put(
        _url + "/999999999", json=updated_supplier, headers=headers
    )
    assert put_response.status_code == 200

    if put_response.content:
        response_data = put_response.json()  # Parse JSON response if body is not empty
        assert response_data["name"] == "testsupplier updated"
    else:
        print("PUT request returned 200 but no response body.")

    requests.delete(_url + "/999999999", headers=headers)


def test_url_suppliers(_url):
    get_response = requests.get(_url + "/url_part2" + "/url_part3", headers=headers)
    assert get_response.status_code == 404


def test_input_suppliers(_url):
    get_response = requests.get(_url + "/seven", headers=headers)
    assert get_response.status_code == 404


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
            response_data["created_at"].split(":")[0]
            == datetime.utcnow().isoformat().split(":")[0]
        )
    else:
        print("get request returned 200 but no response body.")

    requests.delete(_url + "/999999999", headers=headers)
    requests.delete(_url + "/999999999", headers=headers)
