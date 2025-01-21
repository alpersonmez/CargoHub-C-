import pytest
import requests

# API key for authentication
API_KEY = 'a1b2c3d4e5'

@pytest.fixture
def _url():
    return "http://localhost:5000/api/dock"

headers = {
    "Accept": "application/json",
    "User-Agent": "test-agent",
    "API_key": API_KEY,  # API key for authorization
}

def test_dock_post_get_delete(_url):

    new_dock = {
        "warehouse_id": 1,
        "status": "free",
        "description": "Dock for testing"
    }

    post_response = requests.post(_url, json=new_dock, headers=headers)
    assert post_response.status_code == 201

    dock_id = post_response.json()['id']

    get_response = requests.get(f"{_url}/{dock_id}", headers=headers)

    if get_response.content:
        response_data = get_response.json()
        assert response_data["description"] == "Dock for testing"
    else:
        print("GET request returned 200 but no body")

    del_response = requests.delete(f"{_url}/{dock_id}", headers=headers)
    assert del_response.status_code == 204

def test_fetch_dock(_url):
    url = f"{_url}/1"
    response = requests.get(url, headers=headers)

    assert response.status_code in [200, 404], f"Unexpected status code: {response.status_code}"

def test_fetch_nonexistent_dock(_url):
    url = f"{_url}/99999"  
    response = requests.get(url, headers=headers)
    assert response.status_code == 404, f"Expected 404, got: {response.status_code}"

def test_update_dock_invalid_data(_url):

    new_dock = {
        "warehouse_id": 2,
        "description": "Invalid dock"
    }
    post_response = requests.post(_url, json=new_dock, headers=headers)
    dock_id = post_response.json()["id"]

    updated_dock_with_missing_data = {
        "missing_field": "Incorrect update"
    }

    put_response = requests.put(f"{_url}/{dock_id}", json=updated_dock_with_missing_data, headers=headers)
    assert put_response.status_code == 400

    requests.delete(f"{_url}/{dock_id}", headers=headers)

def test_delete_nonexistent_dock(_url):
    url = f"{_url}/99999"  
    response = requests.delete(url, headers=headers)
    assert response.status_code == 404, f"Expected 404, got: {response.status_code}"

def test_fetch_all_docks(_url):
    response = requests.get(_url, headers=headers)
    assert response.status_code == 200, f"Unexpected status code: {response.status_code}"
    assert isinstance(response.json(), list), "Expected a list of docks"

def test_partial_update_dock(_url):
    new_dock = {
        "warehouse_id": 3,
        "status": "free",
        "description": "Partial update test"
    }
    post_response = requests.post(_url, json=new_dock, headers=headers)
    dock_id = post_response.json()['id']

    partial_update = {"status": "occupied"}

    response = requests.put(f"{_url}/{dock_id}", json=partial_update, headers=headers)
    assert response.status_code == 200, f"Unexpected status code: {response.status_code}"
    assert response.json()["status"] == "occupied"

    requests.delete(f"{_url}/{dock_id}", headers=headers)

def test_dock_id_field_validation(_url):
    url = f"{_url}/abc"  # Non-numeric dock ID
    response = requests.get(url, headers=headers)
    assert response.status_code == 400, f"Expected 400 for invalid ID, got: {response.status_code}"

def test_create_multiple_docks_and_fetch(_url):
    dock1 = {
        "warehouse_id": 1,
        "status": "free",
        "description": "Dock A"
    }

    dock2 = {
        "warehouse_id": 2,
        "status": "occupied",
        "description": "Dock B"
    }

    requests.post(_url, json=dock1, headers=headers)
    requests.post(_url, json=dock2, headers=headers)

    response = requests.get(_url, headers=headers)
    docks = response.json()
    assert len(docks) >= 2, "Expected at least 2 docks in the list"
