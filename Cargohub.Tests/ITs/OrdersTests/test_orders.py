import pytest
import requests
from datetime import datetime


@pytest.fixture
def base_url():
    return 'http://localhost:5000/api/order'


headers = {
    "Accept": "/",
    "User-Agent": "value",
    "API_key": "a1b2c3d4",  
}


# Test adding and removing shipment
def test_add_remove_order(base_url):
    new_order = {
        "source_id": 33,
        "order_date": "2019-04-03T11:33:15Z",
        "request_date": "2019-04-07T11:33:15Z",
        "reference": "UPDATED",
        "reference_extra": "Bedreven arm straffen bureau.",
        "order_status": "Delivered",
        "notes": "Voedsel vijf vork heel.",
        "shipping_notes": "Buurman betalen plaats bewolkt.",
        "picking_notes": "Ademen fijn volgorde scherp aardappel op leren.",
        "warehouse_id": 18,
        "ship_to": "123 Main St, Springfield",
        "bill_to": "456 Elm St, Shelbyville",
        "shipment_id": 1,
        "total_amount": 9905.13,
        "total_discount": 150.77,
        "total_tax": 372.72,
        "total_surcharge": 77.6,
        "items": [
            {"ItemId": "P007435", "Amount": 23},
            {"ItemId": "P009557", "Amount": 1}
        ]
    }

    # POST request to add shipment
    post_response = requests.post(base_url, json=new_order, headers=headers)
    assert post_response.status_code == 201
    
    get_response = requests.get(
        base_url + f"/{post_response.json()['id']}", headers=headers
    )

    if get_response.content:
        response_data = get_response.json()
        assert response_data["reference"] == "UPDATED"
    else:
        print("GET request returned 200 but no body")

    del_response = requests.delete(
        base_url + f"/{post_response.json()['id']}", headers=headers
    )
    assert del_response.status_code == 204


# Test updating a shipment
def test_update_order(base_url):
    new_order = {
        "source_id": 33,
        "order_date": "2019-04-03T11:33:15Z",
        "request_date": "2019-04-07T11:33:15Z",
        "reference": "UPDATED",
        "reference_extra": "Bedreven arm straffen bureau.",
        "order_status": "Delivered",
        "notes": "Voedsel vijf vork heel.",
        "shipping_notes": "Buurman betalen plaats bewolkt.",
        "picking_notes": "Ademen fijn volgorde scherp aardappel op leren.",
        "warehouse_id": 18,
        "ship_to": "123 Main St, Springfield",
        "bill_to": "456 Elm St, Shelbyville",
        "shipment_id": 1,
        "total_amount": 9905.13,
        "total_discount": 150.77,
        "total_tax": 372.72,
        "total_surcharge": 77.6,
        "items": [
            {"ItemId": "P007435", "Amount": 23},
            {"ItemId": "P009557", "Amount": 1}
        ]
    }
    
    # POST request to add shipment
    post_response = requests.post(base_url, json=new_order, headers=headers)
    order_id = post_response.json()["id"]

    updated_order = {
        "Id": order_id,
        "source_id": 33,
        "order_date": "2019-04-03T11:33:15Z",
        "request_date": "2019-04-07T11:33:15Z",
        "reference": "UPDATED",
        "reference_extra": "Bedreven arm straffen bureau.",
        "order_status": "Delivered",
        "notes": "Voedsel vijf vork heel.",
        "shipping_notes": "Buurman betalen plaats bewolkt.",
        "picking_notes": "Ademen fijn volgorde scherp aardappel op leren.",
        "warehouse_id": 18,
        "ship_to": "123 Main St, Springfield",
        "bill_to": "456 Elm St, Shelbyville",
        "shipment_id": 1,
        "total_amount": 9905.13,
        "total_discount": 150.77,
        "total_tax": 372.72,
        "total_surcharge": 77.6,
        "items": [
            {"ItemId": "P007435", "Amount": 23},
            {"ItemId": "P009557", "Amount": 1}
        ]
    }

    # PUT request to update shipment
    put_response = requests.put(f"{base_url}/{order_id}", json=updated_order, headers=headers)
    assert put_response.status_code == 204  # Expecting no content after PUT
    
    # Check if the PUT response has a body (it shouldn't)
    if put_response.content:
        response_data = put_response.json()  # Parse JSON response if body is not empty
        assert response_data["reference"] == "UPDATED"
    else:
        print("PUT request returned 200 but no response body.")

    requests.delete(f"{base_url}/{order_id}", headers=headers)


# Test to create an order with missing required fields (invalid creation)
# je krijgt wel een 201 maar hij voegt hem niet toe 
def test_create_order_invalid(base_url):
    invalid_order = {
    "missingData1": "John Doe",
    "missingData2": "Witte de Withstraat 50",
    "missingData3": "Rotterdam",
    }
    # Send a POST request with invalid data
    response = requests.post(base_url, json=invalid_order, headers=headers)

    # Verify the status code is 400 (Bad Request)
    assert response.status_code == 400, f"Expected 400 error, got: {response.status_code}"


# Test to fetch all orders
def test_fetch_all_orders(base_url):

    # Send a GET request to fetch all orders
    response = requests.get(base_url, headers=headers)

    # Verify the status code is 200 (OK)
    assert response.status_code == 200, f"Unexpected status code: {response.status_code}"

    # Check if the response is a list of orders
    orders = response.json()
    assert isinstance(orders, list), "Expected a list of orders"
    assert len(orders) > 0, "Expected at least one order"
