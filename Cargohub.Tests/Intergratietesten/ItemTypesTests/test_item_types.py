import pytest
import requests


@pytest.fixture
def _url():
    return "http://localhost:5000/api/itemtypes"


headers = {
    "Accept": "/",
    "User-Agent": "value",
    "API_key": "a1b2c3d4e5",  #  the API key
}


def test_post_item_type(_url):
    new_item_type = {
        "id": 999999,
        "name": "frontdesk",
        "description": "",
        "created_at": "2020-07-28 13:43:32",
        "updated_at": "2020-07-28 13:43:32",
    }

    post_response = requests.post(_url, json=new_item_type, headers=headers)
    assert post_response.status_code == 201

    get_response = requests.get(_url + "/999999", headers=headers)

    if get_response.content:
        response_data = get_response.json()
        assert response_data["name"] == "frontdesk"
    else:
        print("GET request returned 200 but no body")

    del_response = requests.delete(_url + "/999999", headers=headers)
    assert del_response.status_code == 200


def test_get_item_type(_url):
    response = requests.get(_url + "/2", headers=headers)
    if response.content:
        response_data = response.json()  # Parse JSON response if body is not empty
        assert response_data["id"] == 2
    else:
        print("GET request returned 200 but no response body.")


def test_put_item_type(_url):
    current_type = {
        "id": 1,
        "name": "Desktop",
        "description": "",
        "created_at": "1993-07-28 13:43:32",
        "updated_at": "2022-05-12 08:54:35",
    }

    updated_type = {
        "id": 1,
        "name": "Testing type",
        "description": "This is a itemtype made for the put test.",
        "created_at": "1993-07-28 13:43:32",
        "updated_at": "2022-05-12 08:54:35",
    }
    put_response = requests.put(_url + "/1", json=updated_type, headers=headers)
    assert put_response.status_code == 200

    if put_response.content:
        response_data = put_response.json()  # Parse JSON response if body is not empty
        assert (
            response_data["description"] == "This is a itemtype made for the put test."
        )
    else:
        print("PUT request returned 200 but no response body.")

    requests.put(_url + "/1", json=current_type, headers=headers)


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
