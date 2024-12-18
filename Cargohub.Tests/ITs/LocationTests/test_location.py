import pytest
import requests
from datetime import datetime


@pytest.fixture
def base_url():
    return 'http://localhost:5000/api/location'


headers = {
    "Accept": "/",
    "User-Agent": "value",
    "API_key": "a1b2c3d4",  
}


# Test adding and removing shipment
def test_add_remove_location(base_url):
    new_location = {
        "WareHouseID": 10,
        "Code": "POST",
        "Name": "Johannes"
    }

    # POST request to add shipment
    post_response = requests.post(base_url, json=new_location, headers=headers)
    assert post_response.status_code == 201
    
    get_response = requests.get(
        base_url + f"/{post_response.json()['id']}", headers=headers
    )

    if get_response.content:
        response_data = get_response.json()
        assert response_data["Code"] == "POST"
    else:
        print("GET request returned 200 but no body")

    del_response = requests.delete(
        base_url + f"/{post_response.json()['id']}", headers=headers
    )
    assert del_response.status_code == 204


# Test updating a shipment
def test_update_location(base_url):
    new_location = {
        "WareHouseID": 10,
        "Code": "POST",
        "Name": "Johannes"
    }
    
    # POST request to add shipment
    post_response = requests.post(base_url, json=new_location, headers=headers)
    location_id = post_response.json()["id"]

    updated_location = {
        "Id": location_id,
        "WareHouseID": 10,
        "Code": "POST",
        "Name": "UPDATED"
    }

    # PUT request to update shipment
    put_response = requests.put(f"{base_url}/{location_id}", json=updated_location, headers=headers)
    assert put_response.status_code == 204  # Expecting no content after PUT
    
    # Check if the PUT response has a body (it shouldn't)
    if put_response.content:
        response_data = put_response.json()  # Parse JSON response if body is not empty
        assert response_data["Code"] == "UPDATED"
    else:
        print("PUT request returned 200 but no response body.")

    requests.delete(f"{base_url}/{location_id}", headers=headers)
