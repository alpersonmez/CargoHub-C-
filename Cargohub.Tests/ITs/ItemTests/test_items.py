import pytest
import requests

API_KEY = 'a1b2c3d4e5'


@pytest.fixture
def _url():
    return 'http://localhost:5000/api/v1/'


def test_post_item(_url):
    url = _url + 'items'
    payload = {
        "Uid": "P6942069",
        "Code": "sjQ23408K",
        "Description": "POST",
        "ShortDescription": "must",
        "UpcCode": "6523540947122",
        "ModelNumber": "63-OFFTq0T",
        "CommodityCode": "oTo304",
        "ItemLine": 11,
        "ItemGroup": 73,
        "ItemType": 14,
        "UnitPurchaseQuantity": 47,
        "UnitOrderQuantity": 13,
        "PackOrderQuantity": 11,
        "SupplierId": 34,
        "SupplierCode": "SUP423",
        "SupplierPartNumber": "E-86805-uTM"
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
        "Uid": "P6942069",
        "Code": "sjQ23408K",
        "Description": "POST",
        "ShortDescription": "must",
        "UpcCode": "6523540947122",
        "ModelNumber": "63-OFFTq0T",
        "CommodityCode": "oTo304",
        "ItemLine": 11,
        "ItemGroup": 73,
        "ItemType": 14,
        "UnitPurchaseQuantity": 47,
        "UnitOrderQuantity": 13,
        "PackOrderQuantity": 11,
        "SupplierId": 34,
        "SupplierCode": "SUP423",
        "SupplierPartNumber": "E-86805-uTM",
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
        assert response_data['Description'] == 'POST'
    else:
        print("Item with description POST not found")


def test_put_item(_url):
    url = _url + 'items/P6942069'
    
    updated_payload = {
        "Uid": "P6942069",
        "Code": "sjQ23408K",
        "Description": "UPDATED",
        "ShortDescription": "must",
        "UpcCode": "6523540947122",
        "ModelNumber": "63-OFFTq0T",
        "CommodityCode": "oTo304",
        "ItemLine": 11,
        "ItemGroup": 73,
        "ItemType": 14,
        "UnitPurchaseQuantity": 47,
        "UnitOrderQuantity": 13,
        "PackOrderQuantity": 11,
        "SupplierId": 34,
        "SupplierCode": "SUP423",
        "SupplierPartNumber": "E-86805-uTM"
    }

    # Send a PUT request to update the specific item
    response = requests.put(url, json=updated_payload, headers={'API_KEY': API_KEY})

    # Verify that the status code is 200 (OK), 404 (Not Found), or 500 (Server Error)
    assert response.status_code in [200, 404, 500], f"Unexpected status code: {response.status_code}"


def test_delete_item(_url):
    url = _url + 'items/P6942069'

    # Send a DELETE request to delete a specific item by UID
    response = requests.delete(url, headers={'API_KEY': API_KEY})

    # Verify that the status code is 204
    assert response.status_code in [204], f"Unexpected status code: {response.status_code}"



if __name__ == "__main__":
    pytest.main()
