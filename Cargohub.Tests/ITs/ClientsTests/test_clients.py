import pytest
import requests

#API_KEY = 'a1b2c3d4e5'


@pytest.fixture
def _url():
    return "http://localhost:5000/api/client"

headers = {
    "Accept": "/",
    "User-Agent": "value",
    "API_key": "a1b2c3d4",  #  the API key
}
        
def test_client_post_get_delete(_url):

    new_client = {
    "name": "John Doe",
    "address": "Witte de Withstraat 50",
    "city": "Rotterdam",
    "zip_code": "3012 BT",
    "province": "South Holland",
    "country": "Netherlands",
    "contact_name": "john",
    "contact_phone": "0625169278",
    "contact_email": "johndoe@example.com"
    }

    post_response = requests.post(_url, json=new_client, headers=headers)
    assert post_response.status_code == 201
    
    get_response = requests.get(
        _url + f"/{post_response.json()['id']}", headers=headers
    )

    if get_response.content:
        response_data = get_response.json()
        assert response_data["city"] == "Rotterdam"
    else:
        print("GET request returned 200 but no body")

    del_response = requests.delete(
        _url + f"/{post_response.json()['id']}", headers=headers
    )
    assert del_response.status_code == 204
    
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
    
    new_client = {
    "name": "John Doe",
    "address": "Witte de Withstraat 50",
    "city": "Rotterdam",
    "zip_code": "3012 BT",
    "province": "South Holland",
    "country": "Netherlands",
    "contact_name": "john",
    "contact_phone": "0625169278",
    "contact_email": "johndoe@example.com"
    }
    post_response = requests.post(_url, json=new_client, headers=headers)
    id = post_response.json()["id"]

    updated_client_with_missing_data = {
    "missingData1": "John Doe",
    "missingData2": "Witte de Withstraat 50",
    "missingData3": "Rotterdam",
    }
    put_response = requests.put(f"{_url}/{id}", json=updated_client_with_missing_data, headers=headers)
    assert put_response.status_code == 400

    requests.delete(f"{_url}/{id}", headers=headers)

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
    "name": "John Doe",
    "address": "Witte de Withstraat 50",
    "city": "Rotterdam",
    "zip_code": "3012 BT",
    "province": "South Holland",
    "country": "Netherlands",
    "contact_name": "john",
    "contact_phone": "0625169278",
    "contact_email": "johndoe@example.com"
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
    "name": "John Doe",
    "address": "Witte de Withstraat 50",
    "city": "Rotterdam",
    "zip_code": "3012 BT",
    "province": "South Holland",
    "country": "Netherlands",
    "contact_name": "john",
    "contact_phone": "0625169278",
    "contact_email": "johndoe@example.com"
    }

    client2 = {
    "name": "John Doe",
    "address": "Witte de Withstraat 50",
    "city": "Rotterdam",
    "zip_code": "3012 BT",
    "province": "South Holland",
    "country": "Netherlands",
    "contact_name": "john",
    "contact_phone": "0625169278",
    "contact_email": "johndoe@example.com"
    }

    requests.post(url, json=client1, headers=headers)
    requests.post(url, json=client2, headers=headers)

    # Step 2: Fetch all clients and verify the two were created
    response = requests.get(url, headers=headers)
    clients = response.json()
    assert len(clients) >= 2, "Expected at least 2 clients in the list"


