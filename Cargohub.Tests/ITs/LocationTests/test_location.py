import pytest
import requests
import datetime


@pytest.fixture
def _url():
    return 'http://localhost:3000/api/v1/'


headers = {
            'Accept': '/',
            'User-Agent': 'value',
            'API_KEY': 'a1b2c3d4e5' 
        }

def test_get_all_locations(_url):
    url = _url + 'locations'

    # Send a GET request to the API
    response = requests.get(url, headers=headers)

    # Get the status code and response data
    status_code = response.status_code
    response_data = response.json()

    assert status_code == 200

    # Ensure the response is a list (assuming it returns multiple locations)
    assert isinstance(response_data, list)

    # Check that each location entry in the response has the required fields and correct types
    for location in response_data:
        assert isinstance(location, dict)

        # Check the fields 
        assert 'Id' in location
        assert 'WareHouse_Id' in location
        assert 'Code' in location
        assert 'Name' in location
        assert 'CreatedAt' in location
        assert 'UpdatedAt' in location

        # Check the data types
        assert isinstance(location['Id'], int)
        assert isinstance(location['WareHouse_Id'], int)  
        assert isinstance(location['Code'], str) 
        assert isinstance(location['Name'], str) 
        
        # Check that CreatedAt and UpdatedAt are datetime objects
        assert isinstance(location['CreatedAt'], datetime.datetime) 
        assert isinstance(location['UpdatedAt'], datetime.datetime)  


def test_post_location(_url):
    url = _url + 'locations'

    body = {
        "Id": 999999999,
        "WareHouse_Id": 999999999,
        "Code": "Z.9.1",
        "Name": "Row: Z, Rack: 9, Shelf: 1",
        "CreatedAt": "2024-10-13 03:21:32",
        "UpdatedAt": "2024-10-13 03:21:32"
    }

    # Send a POST request to the API
    response = requests.post(url, json=body, headers=headers)

    # Get the status code 
    status_code = response.status_code
    assert status_code == 201

    # Send a GET request to the API
    response = requests.get(url+"/999999999", headers=headers)

    # Get the status code and response data
    status_code = response.status_code
    response_data = response.json()

    assert status_code == 200

    # Check if the response is a dictionary
    assert isinstance(response_data, dict)

    # Check the fields 
    assert "Id" in response_data
    assert "WareHouse_Id" in response_data
    assert "Code" in response_data
    assert "Name" in response_data
    assert "CreatedAt" in response_data
    assert "UpdatedAt" in response_data

    # Check the data types
    assert isinstance(response_data["Id"], int) 
    assert isinstance(response_data["WareHouse_Id"], int)
    assert isinstance(response_data["Code"], str) 
    assert isinstance(response_data["Name"], str)

    # Check that CreatedAt and UpdatedAt are datetime objects
    assert isinstance(response_data["CreatedAt"], datetime.datetime)
    assert isinstance(response_data["UpdatedAt"], datetime.datetime)  


def test_put_location(_url):
    url = _url + 'locations/999999999'
    
    # Send a GET request to the API
    response = requests.get(url, headers=headers)

    # Get the status code and response data
    status_code = response.status_code
    response_data = response.json()

    assert status_code == 200

    # Check if the response is a dictionary
    assert isinstance(response_data, dict)

    # Check the fields 
    assert "Id" in response_data
    assert "WareHouse_Id" in response_data
    assert "Code" in response_data
    assert "Name" in response_data
    assert "CreatedAt" in response_data
    assert "UpdatedAt" in response_data

    # Check the data types
    assert isinstance(response_data["Id"], int)  
    assert isinstance(response_data["WareHouse_Id"], int)  
    assert isinstance(response_data["Code"], str)
    assert isinstance(response_data["Name"], str)

    # Check that CreatedAt and UpdatedAt are datetime objects
    assert isinstance(response_data["CreatedAt"], datetime.datetime)
    assert isinstance(response_data["UpdatedAt"], datetime.datetime)  

    # Change the body of the test location
    body = {
        "Id": 999999999,
        "WareHouse_Id": 999999888,
        "Code": "X.6.6",
        "Name": "Row: X, Rack: 6, Shelf: 6",
        "CreatedAt": "2024-10-13 03:21:32",
        "UpdatedAt": "2024-10-13 03:21:32"
    }

    # Send a PUT request to the API with the changed body
    response = requests.put(url, json=body, headers=headers)

    # Get the status code
    status_code = response.status_code
    assert status_code == 200

    # Send a GET request to the API
    response = requests.get(url, headers=headers)

    # Get the status code and response data
    status_code = response.status_code
    response_data = response.json()

    assert status_code == 200

    # Check that the updated location entry has the required fields and correct types
    assert isinstance(response_data, dict)

    # Check the fields 
    assert "Id" in response_data
    assert "WareHouse_Id" in response_data
    assert "Code" in response_data
    assert "Name" in response_data
    assert "CreatedAt" in response_data
    assert "UpdatedAt" in response_data

    # Check the data types
    assert isinstance(response_data["Id"], int)
    assert isinstance(response_data["WareHouse_Id"], int)  
    assert isinstance(response_data["Code"], str)  
    assert isinstance(response_data["Name"], str)  

    # Check that CreatedAt and UpdatedAt are datetime objects
    assert isinstance(response_data["CreatedAt"], datetime.datetime)  
    assert isinstance(response_data["UpdatedAt"], datetime.datetime)  
