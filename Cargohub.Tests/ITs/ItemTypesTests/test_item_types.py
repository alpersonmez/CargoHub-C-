import pytest
import requests


@pytest.fixture
def _url():
    return "http://localhost:5000/api/itemtypes"


headers = {
    "Accept": "/",
    "User-Agent": "value",
    "API_key": "a1b2c3d4",  #  the API key
}

def test_get_item_type(_url):
    response = requests.get(_url + "/2", headers=headers)
    if response.content:
        response_data = response.json()  # Parse JSON response if body is not empty
        assert response_data["id"] == 2
    else:
        print("GET request returned 200 but no response body.")


def test_put_item_type(_url):
    updated_type = {
    "id": 3,
    "name": "Updated Electronics",
    "description": "Updated description for electronics"
}
    put_response = requests.put(_url + "/3", json=updated_type, headers=headers)
    assert put_response.status_code == 200


def test_url_item_type(_url):
    get_response = requests.get(_url + "/url_part2" + "/url_part3", headers=headers)
    assert get_response.status_code == 404


def test_input_item_type(_url):
    get_response = requests.get(_url + "/seven", headers=headers)
    assert get_response.status_code == 400


def test_url_specification_item_type(_url):
    get_response = requests.get(
        _url + "/1" + "/utems", headers=headers
    )  # a misspelling for items
    assert get_response.status_code == 404
