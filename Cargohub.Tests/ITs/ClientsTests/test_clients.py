import pytest
import requests

#API_KEY = 'a1b2c3d4e5'


@pytest.fixture
def _url():
    return 'http://localhost:5000/api/v1/'

headers = {
    "Accept": "/",
    "User-Agent": "value",
    "API_key": "a1b2c3d4e5",  #  the API key
}
        
def test_client_post(_url):
    url = _url + 'clients'

    new_client = {
    "Name": "John Doe",
    "Address": "Witte de Withstraat 50",
    "City": "Rotterdam",
    "ZipCode": "3012 BT",
    "Province": "South Holland",
    "Country": "Netherlands",
    "ContactName": "john",
    "ContactPhone": "0625169278",
    "ContactEmail": "johndoe@example.com"
    }

    response = requests.post(url, json=new_client, headers=headers)
    
    print(f"Response status: {response.status_code}")
    print(f"Response body: {response.text}")
    
    # Check if the status code is 201 (Created)
    assert response.status_code == 201 
    
def test_fetch_client(_url):
    url = _url + 'clients'
    params = {'Id': '1'}
    # Send a GET request to fetch an client by id
    response = requests.get(url, params=params ,headers=headers)

    # Verify that the status code is 200 (OK) or 404 (Not Found)
    assert response.status_code in [200, 404], f"Unexpected status code: {response.status_code}"

# Fetch a client that does not exist.
def test_fetch_nonexistent_client(_url):
    url = _url + 'clients/99999'  
    response = requests.get(url, headers=headers)
    assert response.status_code == 404, f"Expected 404, got: {response.status_code}"

# Try to give invalid data, expect 400 error.
def test_update_client_invalid_data(_url):
    url = _url + 'clients/1' 
    invalid_data = {'contact_email': 'invalid-email'}  
    response = requests.put(url, json=invalid_data, headers=headers)
    assert response.status_code == 400, f"Expected 400 for invalid email, got: {response.status_code}"

# Trying to delete not existing client, expect 404
def test_delete_nonexistent_client(_url):
    url = _url + 'clients/99999'  
    response = requests.delete(url, headers=headers)
    assert response.status_code == 404, f"Expected 404, got: {response.status_code}"


def test_fetch_all_clients(_url):
    url = _url + 'clients'
    response = requests.get(url, headers=headers)
    assert response.status_code == 200, f"Unexpected status code: {response.status_code}"
    assert isinstance(response.json(), list), "Expected a list of clients"

# Try to get client by email instead of ID
def test_fetch_client_by_email(_url):
    # Step 1: Create a client to test with
    url = _url + 'clients'
    new_client = {
    "Name": "John Doe",
    "Address": "Witte de Withstraat 50",
    "City": "Rotterdam",
    "ZipCode": "3012 BT",
    "Province": "South Holland",
    "Country": "Netherlands",
    "ContactName": "john",
    "ContactPhone": "0625169278",
    "ContactEmail": "johndoe@example.com"
    }
    response = requests.post(url, json=new_client, headers=headers)
    client_id = response.json()['id']

    # Step 2: Fetch the client by email
    params = {'contact_email': 'johndoe@example.com'}
    response = requests.get(url, params=params, headers=headers)
    assert response.status_code == 200, f"Unexpected status code: {response.status_code}"
    assert response.json()['contact_email'] == 'johndoe@example.com', "Email doesn't match"

def test_partial_update_client(_url):
    url = _url + 'clients/1'
    partial_update = {'name': 'Test'}  # Only update name
    response = requests.put(url, json=partial_update, headers=headers)
    assert response.status_code == 200, f"Unexpected status code: {response.status_code}"
    assert response.json()['name'] == 'Test', "name didn't update"

def test_client_id_field_validation(_url):
    url = _url + 'clients'
    params = 'abc'  # Non-numeric client ID
    response = requests.get(url, params=params, headers=headers)
    assert response.status_code == 400, f"Expected 400 for invalid ID, got: {response.status_code}"

def test_create_multiple_clients_and_fetch(_url):
    url = _url + 'clients'

    # Step 1: Create multiple clients
    
    client1 = {
    "Name": "Alice Smith",
    "Address": "Nieuwe Binnenweg 123",
    "City": "Rotterdam",
    "ZipCode": "3014 GG",
    "Province": "South Holland",
    "Country": "Netherlands",
    "ContactName": "Alice",
    "ContactPhone": "0612345678",
    "ContactEmail": "alice.smith@example.com"
    }

    client2 = {
        "Name": "Bob Johnson",
        "Address": "Blaak 40",
        "City": "Rotterdam",
        "ZipCode": "3011 TA",
        "Province": "South Holland",
        "Country": "Netherlands",
        "ContactName": "Bob",
        "ContactPhone": "0687654321",
        "ContactEmail": "bob.johnson@example.com"
    }

    requests.post(url, json=client1, headers=headers)
    requests.post(url, json=client2, headers=headers)

    # Step 2: Fetch all clients and verify the two were created
    response = requests.get(url, headers=headers)
    clients = response.json()
    assert len(clients) >= 2, "Expected at least 2 clients in the list"


