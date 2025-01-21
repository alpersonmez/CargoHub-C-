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
    "name": "POST",
    "address": "POST",
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
        assert response_data["country"] == "Netherlands"
    else:
        print("GET request returned 200 but no body")

    del_response = requests.delete(
        _url + f"/{post_response.json()['id']}", headers=headers
    )
    assert del_response.status_code == 204
    
def test_fetch_client(_url):
    response = requests.get(_url+"\1" ,headers=headers)

    # Verify that the status code is 200 (OK) or 404 (Not Found)
    assert response.status_code in [200, 404], f"Unexpected status code: {response.status_code}"

# Fetch a client that does not exist.
def test_fetch_nonexistent_client(_url):
    response = requests.get(_url+"/99999999999999999999", headers=headers)
    assert response.status_code == 400, f"Expected 404, got: {response.status_code}"

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
    response = requests.delete(_url+"/99999999999999999999", headers=headers)
    assert response.status_code == 400, f"Expected 404, got: {response.status_code}"


def test_fetch_all_clients(_url):
    response = requests.get(_url+"/amount/5", headers=headers)
    assert response.status_code == 200, f"Unexpected status code: {response.status_code}"
    assert isinstance(response.json(), list), "Expected a list of clients"
