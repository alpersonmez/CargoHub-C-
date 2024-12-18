import pytest
import requests


@pytest.fixture
def _url():
    return "http://localhost:5000/api/inventory"


headers = {
    "Accept": "/",
    "API_key": "a1b2c3d4e5",  #  the API key
}


def test_add_remove_inventories(_url):

    new_inv = {
        "id": 99999999,
        "item_id": "P0000898",
        "description": "test inventorie",
    }

    post_response = requests.post(_url, json=new_inv, headers=headers)
    assert post_response.status_code == 201
    # data = response.json()
    # assert "id" in data
    # assert data["description"] == "test inventorie"

    get_response = requests.get(_url + "/99999999", headers=headers)
    assert get_response.status_code == 200

    del_response = requests.delete(_url + "/99999999", headers=headers)
    assert del_response.status_code == 200

    get_response2 = requests.get(_url + "/99999999", headers=headers)
    if get_response2.content:
        response_data = get_response2.json()  # Parse JSON response if body is not empty
        assert response_data == None
    else:
        print("GET request returned 200 but no response body.")


def test_put_inventories(_url):
    new_inv = {
        "id": 99999999,
        "item_id": "P0000899",
        "description": "test inventorie",
    }
    requests.post(_url, json=new_inv, headers=headers)

    updated_inv = {
        "id": 99999999,
        "item_id": "P0000900",
        "description": "test inventories",
    }
    put_response = requests.put(_url + "/99999999", json=updated_inv, headers=headers)
    assert put_response.status_code == 200

    if put_response.content:
        response_data = put_response.json()  # Parse JSON response if body is not empty
        assert response_data == {
            "id": 99999999,
            "item_id": "P0000900",
            "description": "test inventories",
        }
    else:
        print("PUT request returned 200 but no response body.")

    requests.delete(_url + "/99999999", headers=headers)


def test_url_inventories(_url):
    get_response = requests.get(_url + "/url_part2" + "/url_part3", headers=headers)
    assert get_response.status_code == 404


def test_input_inventories(_url):
    get_response = requests.get(_url + "/seven", headers=headers)
    assert get_response.status_code == 404
