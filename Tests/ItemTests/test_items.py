import pytest
import requests

API_KEY = 'a1b2c3d4e5'


@pytest.fixture
def _url():
    return 'http://localhost:5000/api/v1/'


def test_post_item(_url):
    url = _url + 'items'
    payload = {
        "uid": "P6942069",
        "code": "sjQ23408K",
        "description": "POST",
        "short_description": "must",
        "upc_code": "6523540947122",
        "model_number": "63-OFFTq0T",
        "commodity_code": "oTo304",
        "item_line": 11,
        "item_group": 73,
        "item_type": 14,
        "unit_purchase_quantity": 47,
        "unit_order_quantity": 13,
        "pack_order_quantity": 11,
        "supplier_id": 34,
        "supplier_code": "SUP423",
        "supplier_part_number": "E-86805-uTM"
    }

    # Send a POST request to the API
    response = requests.post(url, headers={'API_KEY': API_KEY}, json=payload)

    # Get the status code
    status_code = response.status_code

    # Verify that the status code is 201 (Created)
    assert status_code == 201


def test_post_item_with_invalid_dates(_url):
    url = _url + 'items'
    payload = {
        "uid": "P6942069",
        "code": "sjQ23408K",
        "description": "POST",
        "short_description": "must",
        "upc_code": "6523540947122",
        "model_number": "63-OFFTq0T",
        "commodity_code": "oTo304",
        "item_line": 11,
        "item_group": 73,
        "item_type": 14,
        "unit_purchase_quantity": 47,
        "unit_order_quantity": 13,
        "pack_order_quantity": 11,
        "supplier_id": 34,
        "supplier_code": "SUP423",
        "supplier_part_number": "E-86805-uTM",
        "created_at": "2024-01-01T00:00:00",  # Invalid field
        "updated_at": "2024-01-01T00:00:00"   # Invalid field
    }

    # Send a POST request to the API
    response = requests.post(url, headers={'API_KEY': API_KEY}, json=payload)

    # Verify that the status code is 400 (Bad Request)
    assert response.status_code == 400


def test_get_item_by_uid(_url):
    url = _url + 'items/P6942069'

    # Send a GET request to fetch an item by UID
    response = requests.get(url, headers={'API_KEY': API_KEY})

    # Verify that the status code is 200 (OK) or 404 (Not Found)
    assert response.status_code in [200, 404], f"Unexpected status code: {response.status_code}"

    if response.status_code == 200:
        response_data = response.json()
        assert response_data['description'] == 'POST'
    else:
        print("Item with description POST not found")


def test_put_item(_url):
    url = _url + 'items/P6942069'
    
    updated_payload = {
        "uid": "P6942069",
        "code": "sjQ23408K",
        "description": "UPDATED",
        "short_description": "must",
        "upc_code": "6523540947122",
        "model_number": "63-OFFTq0T",
        "commodity_code": "oTo304",
        "item_line": 11,
        "item_group": 73,
        "item_type": 14,
        "unit_purchase_quantity": 47,
        "unit_order_quantity": 13,
        "pack_order_quantity": 11,
        "supplier_id": 34,
        "supplier_code": "SUP423",
        "supplier_part_number": "E-86805-uTM"
    }

    # Send a PUT request to update the specific item
    response = requests.put(url, json=updated_payload, headers={'API_KEY': API_KEY})

    # Verify that the status code is 200 (OK), 404 (Not Found), or 500 (Server Error)
    assert response.status_code in [200, 404, 500], f"Unexpected status code: {response.status_code}"


def test_delete_item(_url):
    url = _url + 'items/P6942069'

    # Send a DELETE request to delete a specific item by UID
    response = requests.delete(url, headers={'API_KEY': API_KEY})

    # Verify that the status code is 200 (OK) or 404 (Not Found)
    assert response.status_code in [200, 404], f"Unexpected status code: {response.status_code}"


if __name__ == "__main__":
    pytest.main()
