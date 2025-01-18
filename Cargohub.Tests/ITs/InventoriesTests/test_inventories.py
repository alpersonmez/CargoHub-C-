import pytest
import requests
from datetime import datetime


@pytest.fixture
def _url():
    return "http://localhost:5000/api/inventory"


headers = {
    "Accept": "/",
    "User-Agent": "value",
    "API_key": "a1b2c3d4",  #  the API key
}


def test_add_remove_inventories(_url):

    new_inventories = {
        "item_id": "P000001",
        "description": "POST",
        "item_reference": "sjQ23408K",
        "locations": [
            3211,
            24700,
            14123,
            19538,
            31071,
            24701,
            11606,
            11817
        ],
        "total_on_hand": 262,
        "total_expected": 0,
        "total_ordered": 80,
        "total_allocated": 41,
        "total_available": 141
    }

    post_response = requests.post(_url, json=new_inventories, headers=headers)
    assert post_response.status_code == 201 

    get_response = requests.get(
        _url + f"/{post_response.json()['id']}", headers=headers
    )

    if get_response.content:
        response_data = get_response.json()
        assert response_data["description"] == "POST"
    else:
        print("GET request returned 200 but no body")

    del_response = requests.delete(
        _url + f"/{post_response.json()['id']}", headers=headers
    )
    assert del_response.status_code == 204


def test_put_inventories(_url):
    new_inventories = {
        "item_id": "P000001",
        "description": "POST",
        "item_reference": "sjQ23408K",
        "locations": [
            3211,
            24700,
            14123,
            19538,
            31071,
            24701,
            11606,
            11817
        ],
        "total_on_hand": 262,
        "total_expected": 0,
        "total_ordered": 80,
        "total_allocated": 41,
        "total_available": 141
    }
    post_response = requests.post(_url, json=new_inventories, headers=headers)
    id = post_response.json()["id"]

    updated_inventories = {
        "id": id,
        "item_id": "P000001",
        "description": "UPDATED",
        "item_reference": "sjQ23408K",
        "locations": [
            3211,
            24700,
            14123,
            19538,
            31071,
            24701,
            11606,
            11817
        ],
        "total_on_hand": 262,
        "total_expected": 0,
        "total_ordered": 80,
        "total_allocated": 41,
        "total_available": 141
    }
    put_response = requests.put(f"{_url}/{id}", json=updated_inventories, headers=headers)
    assert put_response.status_code == 204

    if put_response.content:
        response_data = put_response.json()  # Parse JSON response if body is not empty
        assert response_data["description"] == "UPDATED"
    else:
        print("PUT request returned 200 but no response body.")

    requests.delete(f"{_url}/{id}", headers=headers)


def test_url_inventories(_url):
    get_response = requests.get(_url + "/url_part2" + "/url_part3", headers=headers)
    assert get_response.status_code == 404


def test_input_inventories(_url):
    get_response = requests.get(_url + "/seven", headers=headers)
    assert get_response.status_code == 400


def test_url_specification_inventories(_url):
    get_response = requests.get(
        _url + "/1" + "/utems", headers=headers
    )  # a misspelling for items
    assert get_response.status_code == 404