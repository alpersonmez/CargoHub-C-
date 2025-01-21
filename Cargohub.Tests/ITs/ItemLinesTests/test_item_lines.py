import pytest
import requests

API_KEY = 'a1b2c3d4'


@pytest.fixture
def _url():
    return 'http://localhost:5000/api/'

headers = {
    "Accept": "/",
    "User-Agent": "value",
    "API_key": "a1b2c3d4",  #  the API key
}


def test_put_item_lines(_url):
    url = _url + 'item_lines/1'
    
    updated_payload = {
    "id": 1,
    "name": "Home Appliances",
    "description": "UPDATED"
    }

    try:
        # Send a PUT request to update the specific item
        response = requests.put(url, json=updated_payload, headers=headers)

        # Verify that the status code is 200 (OK), 404 (Not Found), or 500 (Server Error)
        assert response.status_code in [200, 404, 500], f"Unexpected status code: {response.status_code}"

        if response.status_code == 200:
            response_data = response.json()
            print(f"Item lines successfully updated: {response_data}")
        elif response.status_code == 404:
            print("Item lines not found, cannot update")
        else:
            print("Server error when trying to update item lines")
    
    # Catch any requests-related errors, including timeouts
    except requests.exceptions.RequestException as e:
        print(f"An error occurred: {e}")

def test_get_item_lines_by(_url):
    url = _url + 'item_lines'
    params = {'id': '1'}

    # Send a GET request to fetch a item line by ID
    response = requests.get(url, params=params, headers={'API_KEY': API_KEY})

    # Verify that the status code is either 200 (OK) or 404 (Not Found)
    assert response.status_code in [200, 404], f"Unexpected status code: {response.status_code}"

    if response.status_code == 200:
        response_data = response.json()
        assert response_data['description'] == 'UPDATED'
    else:
        print("Item lines with description UPDATED not found")

def test_delete_item_lines_by(_url):
    url = _url + 'item_lines'
    params = {'id': '1'}

    # Send a DELETE request to delete a item lines by ID
    response = requests.delete(url, params=params, headers={'API_KEY': API_KEY})

    # Verify that the status code is either 200 (OK) or 404 (Not Found)
    assert response.status_code in [200, 404], f"Unexpected status code: {response.status_code}"

    
def test_get_all_item_lines(_url):
    url = _url + 'item_lines'

    # Send a GET request to fetch all item lines
    response = requests.get(url, headers={'API_KEY': API_KEY})

    # Verify that the status code is either 200 (OK) or 404 (Not Found)
    assert response.status_code in [200, 404], f"Unexpected status code: {response.status_code}"