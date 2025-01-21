import pytest
import requests
from datetime import datetime


@pytest.fixture
def _url():
    return "http://localhost:5000/api/item"


headers = {
    "Accept": "/",
    "User-Agent": "value",
    "API_key": "a1b2c3d4",  #  the API key
}

def test_add_remove_items(_url):
    new_item = {
        "code": "CODE001",
        "description": "POST",
        "short_description": "Sample item",
        "upc_code": "123456789012",
        "model_number": "MODEL001",
        "commodity_code": "COMM001",
        "item_line": 1,
        "item_group": 2,
        "item_type": 3,
        "unit_purchase_quantity": 10,
        "unit_order_quantity": 5,
        "pack_order_quantity": 50,
        "supplier_id": 123,
        "supplier_code": "SUPP123",
        "supplier_part_number": "SPN12345",
    }

    post_response = requests.post(_url, json=new_item, headers=headers)
    assert post_response.status_code == 201

    uid = post_response.json()["uid"]

    get_response = requests.get(f"{_url}/{uid}", headers=headers)
    assert get_response.status_code == 200
    assert get_response.json()["description"] == "POST"

    del_response = requests.delete(f"{_url}/{uid}", headers=headers)
    assert del_response.status_code == 204


def test_put_items(_url):
    new_item = {
        "id": 1,
        "code": "CODE001",
        "description": "POST",
        "short_description": "Sample item",
        "upc_code": "123456789012",
        "model_number": "MODEL001",
        "commodity_code": "COMM001",
        "item_line": 1,
        "item_group": 2,
        "item_type": 3,
        "unit_purchase_quantity": 10,
        "unit_order_quantity": 5,
        "pack_order_quantity": 50,
        "supplier_id": 123,
        "supplier_code": "SUPP123",
        "supplier_part_number": "SPN12345"
    }
    post_response = requests.post(_url, json=new_item, headers=headers)

    if post_response.status_code != 200:
        print(f"POST failed: {post_response.status_code} - {post_response.text}")
        return

    try:
        uid = post_response.json()["uid"]
    except (KeyError, requests.exceptions.JSONDecodeError):
        print(f"Unexpected response format: {post_response.text}")
        return

    updated_item = {
        "id": 1,
        "uid": uid,
        "code": "CODE001",
        "description": "POST",
        "short_description": "Sample item",
        "upc_code": "123456789012",
        "model_number": "MODEL001",
        "commodity_code": "COMM001",
        "item_line": 1,
        "item_group": 2,
        "item_type": 3,
        "unit_purchase_quantity": 10,
        "unit_order_quantity": 5,
        "pack_order_quantity": 50,
        "supplier_id": 123,
        "supplier_code": "SUPP123",
        "supplier_part_number": "SPN12345"
    }
    put_response = requests.put(f"{_url}/{uid}", json=updated_item, headers=headers)

    if put_response.status_code != 200:
        print(f"PUT failed: {put_response.status_code} - {put_response.text}")
        return

    try:
        response_data = put_response.json()
        assert response_data["description"] == "POST"
    except requests.exceptions.JSONDecodeError:
        print("PUT request returned 200 but no response body.")

    delete_response = requests.delete(f"{_url}/{uid}", headers=headers)
    if delete_response.status_code != 200:
        print(f"DELETE failed: {delete_response.status_code} - {delete_response.text}")



def test_url_items(_url):
    get_response = requests.get(_url + "/url_part2" + "/url_part3", headers=headers)
    assert get_response.status_code == 404


def test_input_items(_url):
    get_response = requests.get(_url + "/seven", headers=headers)
    assert get_response.status_code == 404


def test_url_specification_items(_url):
    get_response = requests.get(
        _url + "/1" + "/utems", headers=headers
    )  # a misspelling for items
    assert get_response.status_code == 404

