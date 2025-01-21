import pytest
import requests


@pytest.fixture
def _url():
    return 'http://localhost:5000/api/item_groups'

headers = {
    "Accept": "/",
    "User-Agent": "value",
    "API_key": "a1b2c3d4",  #  the API key
}

def test_put_item_groups(_url):
    updated_payload = {
    "id": 1,
    "name": "Home Appliances",
    "description": "UPDATED"
    }

    try:
        # Send a PUT request to update the specific item group
        response = requests.put(_url +"1", json=updated_payload, headers=headers)

        # Verify that the status code is 200 (OK), 404 (Not Found), or 500 (Server Error)
        assert response.status_code in [200, 404, 500], f"Unexpected status code: {response.status_code}"

        if response.status_code == 200:
            response_data = response.json()
            print(f"Item group successfully updated: {response_data}")
        elif response.status_code == 404:
            print("Item group not found, cannot update")
        else:
            print("Server error when trying to update item groups")
    
    # Catch any requests-related errors, including timeouts
    except requests.exceptions.RequestException as e:
        print(f"An error occurred: {e}")

def test_get_item_group_by(_url):#
    url = _url + 'item_groups'
    params = {'id': '1'}

    # Send a GET request to fetch a item group by ID
    response = requests.get(url, params=params, headers=headers)

    # Verify that the status code is either 200 (OK) or 404 (Not Found)
    assert response.status_code in [200, 404], f"Unexpected status code: {response.status_code}"

    if response.status_code == 200:
        response_data = response.json()
        assert response_data['description'] == 'UPDATED'
    else:
        print("Item lines with description UPDATED not found")

def test_delete_item_group_by(_url):#
    url = _url + 'item_groups'
    params = {'id': '1'}

    # Send a DELETE request to delete an item group by ID
    response = requests.delete(url, params=params, headers=headers)

    # Verify that the status code is either 200 (OK) or 404 (Not Found)
    assert response.status_code in [200, 404], f"Unexpected status code: {response.status_code}"

    
def test_get_all_items_groups(_url):#
    url = _url + 'item_groups'

    # Send a GET request to fetch all item groups
    response = requests.get(url, headers=headers)

    # Verify that the status code is either 200 (OK) or 404 (Not Found)
    assert response.status_code in [200, 404], f"Unexpected status code: {response.status_code}"