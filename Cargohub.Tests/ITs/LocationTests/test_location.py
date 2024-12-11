import pytest
import requests
import datetime


@pytest.fixture
def url():
    return 'http://localhost:5000/api/v1/locations'


headers = {
    "Accept": "/",
    "User-Agent": "value",
    "API_key": "a1b2c3d4",  #  the API key
}

def test_get_all_locations(url):

    # Send a GET request to the API
    response = requests.get(url, headers=headers)

    # Get the status code and response data
    status_code = response.status_code
    response_data = response.json()

    assert status_code == 200

    # # Ensure the response is a list (assuming it returns multiple locations)
    # assert isinstance(response_data, list)

    # # Check that each location entry in the response has the required fields and correct types
    # for location in response_data:
    #     assert isinstance(location, dict)

    #     # Check the fields 
    #     assert 'Id' in location
    #     assert 'WareHouse_Id' in location
    #     assert 'Code' in location
    #     assert 'Name' in location
    #     assert 'CreatedAt' in location
    #     assert 'UpdatedAt' in location

    #     # Check the data types
    #     assert isinstance(location['Id'], int)
    #     assert isinstance(location['WareHouse_Id'], int)  
    #     assert isinstance(location['Code'], str) 
    #     assert isinstance(location['Name'], str) 
        
    #     # Check that CreatedAt and UpdatedAt are datetime objects
    #     assert isinstance(location['CreatedAt'], datetime.datetime) 
    #     assert isinstance(location['UpdatedAt'], datetime.datetime)  


def test_post_location(url):

    body = {
        "WareHouse_Id": 999999999,
        "Code": "Z.9.1",
        "Name": "Row: Z, Rack: 9, Shelf: 1",
    }

    # Send a POST request to the API
    post_response = requests.post(url, json=body, headers=headers)

    # Get the status code 
    status_code = post_response.status_code
    assert status_code == 201
    id = post_response.json()["id"]

    # Send a GET request to the API
    response = requests.get(f"{url}/{id}", headers=headers)

    # Get the status code and response data
    status_code = response.status_code
    response_data = response.json()

    assert status_code == 200

    # # Check if the response is a dictionary
    # assert isinstance(response_data, dict)

    # # Check the fields 
    # assert "Id" in response_data
    # assert "WareHouse_Id" in response_data
    # assert "Code" in response_data
    # assert "Name" in response_data
    # assert "CreatedAt" in response_data
    # assert "UpdatedAt" in response_data

    # # Check the data types
    # assert isinstance(response_data["Id"], int) 
    # assert isinstance(response_data["WareHouse_Id"], int)
    # assert isinstance(response_data["Code"], str) 
    # assert isinstance(response_data["Name"], str)

    # # Check that CreatedAt and UpdatedAt are datetime objects
    # assert isinstance(response_data["CreatedAt"], datetime.datetime)
    # assert isinstance(response_data["UpdatedAt"], datetime.datetime)  


def test_put_location(url):
    
    POSTbody = {
        "WareHouse_Id": 911,
        "Code": "Z.9.1",
        "Name": "Row: Z, Rack: 9, Shelf: 1",
    }
    
    # Send a POST request to the API
    post_response = requests.post(url, json=POSTbody, headers=headers)

    # Get the status code 
    status_code = post_response.status_code
    assert status_code == 201
    id = post_response.json()["id"]

    # Send a GET request to the API
    response = requests.get(f"{url}/{id}", headers=headers)

    # Get the status code and response data
    status_code = response.status_code
    response_data = response.json()
    assert status_code == 200

    # # Check if the response is a dictionary
    # assert isinstance(response_data, dict)

    # # Check the fields 
    # assert "Id" in response_data
    # assert "WareHouse_Id" in response_data
    # assert "Code" in response_data
    # assert "Name" in response_data
    # assert "CreatedAt" in response_data
    # assert "UpdatedAt" in response_data

    # # Check the data types
    # assert isinstance(response_data["Id"], int)  
    # assert isinstance(response_data["WareHouse_Id"], int)  
    # assert isinstance(response_data["Code"], str)
    # assert isinstance(response_data["Name"], str)

    # # Check that CreatedAt and UpdatedAt are datetime objects
    # assert isinstance(response_data["CreatedAt"], datetime.datetime)
    # assert isinstance(response_data["UpdatedAt"], datetime.datetime)  

    # Change the body of the test location
    PUTbody = {
        "Id": id,
        "WareHouse_Id": 911,
        "Code": "Z.9.1",
        "Name": "Row: Z, Rack: 9, Shelf: 1",
    }

    # Send a PUT request to the API with the changed body
    put_response = requests.put(f"{url}/{id}", json=PUTbody, headers=headers)
    assert put_response.status_code == 204

    if put_response.content:
        response_data = put_response.json()  # Parse JSON response if body is not empty
        assert response_data["WareHouse_Id"] == 911
    else:
        print("PUT request returned 200 but no response body.")

    # Send a GET request to the API
    response = requests.get(f"{url}/{id}", headers=headers)

    # Get the status code and response data
    status_code = response.status_code
    response_data = response.json()

    assert status_code == 200

    # # Check that the updated location entry has the required fields and correct types
    # assert isinstance(response_data, dict)

    # # Check the fields 
    # assert "Id" in response_data
    # assert "WareHouse_Id" in response_data
    # assert "Code" in response_data
    # assert "Name" in response_data
    # assert "CreatedAt" in response_data
    # assert "UpdatedAt" in response_data

    # # Check the data types
    # assert isinstance(response_data["Id"], int)
    # assert isinstance(response_data["WareHouse_Id"], int)  
    # assert isinstance(response_data["Code"], str)  
    # assert isinstance(response_data["Name"], str)  

    # # Check that CreatedAt and UpdatedAt are datetime objects
    # assert isinstance(response_data["CreatedAt"], datetime.datetime)  
    # assert isinstance(response_data["UpdatedAt"], datetime.datetime)  
