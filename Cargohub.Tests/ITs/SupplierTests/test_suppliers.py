import pytest
import requests
from datetime import datetime


@pytest.fixture
def _url():
    return "http://localhost:5000/api/supplier"


headers = {
    "Accept": "/",
    "User-Agent": "value",
    "API_key": "a1b2c3d4",  #  the API key
}


def test_add_remove_suppliers(_url):

    new_supplier = {
        "code": "SUP0001",
        "name": "testsupplier",
        "address": "unknown",
        "address_extra": "Apt. 420",
        "city": "Port Anitaburgh",
        "zip_code": "91688",
        "province": "Illinois",
        "country": "Czech Republic",
        "contact_name": "Toni Barnett",
        "phone_number": "363.541.7282x36825",
        "reference": "LPaJ-SUP0001",
    }

    post_response = requests.post(_url, json=new_supplier, headers=headers)
    assert post_response.status_code == 201

    get_response = requests.get(
        _url + f"/{post_response.json()['id']}", headers=headers
    )

    if get_response.content:
        response_data = get_response.json()
        assert response_data["name"] == "testsupplier"
    else:
        print("GET request returned 200 but no body")

    del_response = requests.delete(
        _url + f"/{post_response.json()['id']}", headers=headers
    )
    assert del_response.status_code == 204


def test_put_suppliers(_url):
    new_supplier = {
        "code": "SUP0001",
        "name": "testsupplier",
        "address": "unknown",
        "address_extra": "Apt. 420",
        "city": "Port Anitaburgh",
        "zip_code": "91688",
        "province": "Illinois",
        "country": "Czech Republic",
        "contact_name": "Toni Barnett",
        "phone_number": "363.541.7282x36825",
        "reference": "LPaJ-SUP0001",
    }
    post_response = requests.post(_url, json=new_supplier, headers=headers)
    id = post_response.json()["id"]

    updated_supplier = {
        "Id": id,
        "code": "SUP0001",
        "name": "testsupplier",
        "address": "unknown",
        "address_extra": "Apt. 420",
        "city": "Port Anitaburgh",
        "zip_code": "91688",
        "province": "Illinois",
        "country": "Czech Republic",
        "contact_name": "Toni Barnett",
        "phone_number": "363.541.7282x36825",
        "reference": "LPaJ-SUP0001",
    }
    put_response = requests.put(f"{_url}/{id}", json=updated_supplier, headers=headers)
    assert put_response.status_code == 204

    if put_response.content:
        response_data = put_response.json()  # Parse JSON response if body is not empty
        assert response_data["address"] == "Hofplein"
    else:
        print("PUT request returned 200 but no response body.")

    requests.delete(f"{_url}/{id}", headers=headers)


def test_url_suppliers(_url):
    get_response = requests.get(_url + "/url_part2" + "/url_part3", headers=headers)
    assert get_response.status_code == 404


def test_input_suppliers(_url):
    get_response = requests.get(_url + "/seven", headers=headers)
    assert get_response.status_code == 400


def test_url_specification_suppliers(_url):
    get_response = requests.get(
        _url + "/1" + "/utems", headers=headers
    )  # a misspelling for items
    assert get_response.status_code == 404


# def test_time_suppliers(_url):
#     new_supplier = {
#         "Code": "C001",
#         "Name": "Alice Smith",
#         "Address": "Nieuwe Binnenweg 123",
#         "AddressExtra": "Apt 5B",
#         "City": "Rotterdam",
#         "ZipCode": "3014 GG",
#         "Country": "Netherlands",
#         "ContactName": "Alice",
#         "PhoneNumber": "0612345678",
#         "Reference": "First-time client"
#     }

#     # Create a new supplier
#     post_response = requests.post(_url, headers=headers, json=new_supplier, )

#     # Check if the POST request was successful
#     assert post_response.status_code == 201, f"Failed to create supplier: {post_response.text}"

#     # Extract the new supplier ID from the response
#     created_supplier = post_response.json()  # Assuming the response body contains the new supplier object
#     new_id = created_supplier.get("id")
#     assert new_id, "Supplier ID not found in the POST response."

#     # GET the newly created supplier
#     get_result = requests.get(f"{_url}/{new_id}", headers=headers)
#     assert get_result.status_code == 200, f"GET request failed: {get_result.text}"

#     # Parse the GET response
#     if get_result.content:
#         response_data = get_result.json()  # Parse JSON response if body is not empty
#         assert response_data["Address"] == "Nieuwe Binnenweg 123", "Address does not match expected value."
#     else:
#         print("GET request returned 200 but no response body.")

#     # DELETE the newly created supplier
#     delete_result = requests.delete(f"{_url}/{new_id}", headers=headers)
#     assert delete_result.status_code == 204, f"DELETE request failed: {delete_result.text}"
