import pytest
import requests
from datetime import datetime


@pytest.fixture
def _url():
    return "http://localhost:5000/api/location"


headers = {
    "Accept": "/",
    "User-Agent": "value",
    "API_key": "a1b2c3d4",  #  the API key
}


def test_add_remove_location(_url):

    new_location = {
        "warehouse_id": 10,
        "code": "LOC001",
        "name": "POST",
        "address": "123 Logistics St.",
        "address_extra": "Suite 100",
        "city": "Port Logistics",
        "zip_code": "56789",
        "province": "California",
        "country": "United States",
        "contact_name": "John Doe",
        "phone_number": "123-456-7890",
        "reference": "REF-LOC001",
    }

    post_response = requests.post(_url, json=new_location, headers=headers)
    assert post_response.status_code == 201

    get_response = requests.get(
        _url + f"/{post_response.json()['id']}", headers=headers
    )

    if get_response.content:
        response_data = get_response.json()
        assert response_data["name"] == "POST"
    else:
        print("GET request returned 200 but no body")

    del_response = requests.delete(
        _url + f"/{post_response.json()['id']}", headers=headers
    )
    assert del_response.status_code == 204


def test_put_location(_url):
    
    new_location = {
        "warehouse_id": 10,
        "code": "LOC001",
        "name": "POST",
        "address": "123 Logistics St.",
        "address_extra": "Suite 100",
        "city": "Port Logistics",
        "zip_code": "56789",
        "province": "California",
        "country": "United States",
        "contact_name": "John Doe",
        "phone_number": "123-456-7890",
        "reference": "REF-LOC001",
    }

    post_response = requests.post(_url, json=new_location, headers=headers)
    id = post_response.json()["id"]

    new_location = {
        "id": id,
        "warehouse_id": 10,
        "code": "LOC001",
        "name": "UPDATED",
        "address": "123 Logistics St.",
        "address_extra": "Suite 100",
        "city": "Port Logistics",
        "zip_code": "56789",
        "province": "California",
        "country": "United States",
        "contact_name": "John Doe",
        "phone_number": "123-456-7890",
        "reference": "REF-LOC001",
    }

    put_response = requests.put(f"{_url}/{id}", json=new_location, headers=headers)
    assert put_response.status_code == 200

    if put_response.content:
        response_data = put_response.json()  # Parse JSON response if body is not empty
        assert response_data["name"] == "UPDATED"
    else:
        print("PUT request returned 200 but no response body.")

    requests.delete(f"{_url}/{id}", headers=headers)


def test_url_locations(_url):
    get_response = requests.get(_url + "/url_part2" + "/url_part3", headers=headers)
    assert get_response.status_code == 404


def test_input_locations(_url):
    get_response = requests.get(_url + "/seven", headers=headers)
    assert get_response.status_code == 400


def test_url_specification_locations(_url):
    get_response = requests.get(
        _url + "/1" + "/utems", headers=headers
    )  # a misspelling for items
    assert get_response.status_code == 404