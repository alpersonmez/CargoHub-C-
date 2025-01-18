import pytest
import requests
from datetime import datetime


@pytest.fixture
def base_url():
    return 'http://localhost:5000/api/shipment'


headers = {
    "Accept": "/",
    "User-Agent": "value",
    "API_key": "a1b2c3d4",  
}


# Test adding and removing shipment
def test_add_remove_shipment(base_url):
    new_shipment = {
        "OrderId": 101,
        "SourceId": 1,
        "OrderDate": "2024-12-10T10:00:00",
        "RequestDate": "2024-12-11T09:00:00",
        "ShipmentDate": "2024-12-12T15:00:00",
        "ShipmentType": "Express",
        "shipment_status": "Shipped",
        "Notes": "Handle with care. Fragile items.",
        "CarrierCode": "DHL123",
        "CarrierDescription": "DHL Express",
        "ServiceCode": "EXP001",
        "PaymentType": "Credit Card",
        "TransferMode": "Air",
        "TotalPackageCount": 3,
        "TotalPackageWeight": 12.5
    }

    # POST request to add shipment
    post_response = requests.post(base_url, json=new_shipment, headers=headers)
    assert post_response.status_code == 201
    
    get_response = requests.get(
        base_url + f"/{post_response.json()['id']}", headers=headers
    )

    if get_response.content:
        response_data = get_response.json()
        assert response_data["shipment_status"] == "Shipped"
    else:
        print("GET request returned 200 but no body")

    del_response = requests.delete(
        base_url + f"/{post_response.json()['id']}", headers=headers
    )
    assert del_response.status_code == 204


# Test updating a shipment
def test_update_shipment(base_url):
    new_shipment = {
        "OrderId": 101,
        "SourceId": 1,
        "OrderDate": "2024-12-10T10:00:00",
        "RequestDate": "2024-12-11T09:00:00",
        "ShipmentDate": "2024-12-12T15:00:00",
        "ShipmentType": "Express",
        "ShipmentStatus": "Shipped",
        "Notes": "Handle with care. Fragile items.",
        "CarrierCode": "DHL123",
        "CarrierDescription": "DHL Express",
        "ServiceCode": "EXP001",
        "PaymentType": "Credit Card",
        "TransferMode": "Air",
        "TotalPackageCount": 3,
        "TotalPackageWeight": 12.5
    }
    
    # POST request to add shipment
    post_response = requests.post(base_url, json=new_shipment, headers=headers)
    shipment_id = post_response.json()["id"]

    updated_shipment = {
        "Id": shipment_id,
        "OrderId": 101,
        "SourceId": 1,
        "OrderDate": "2024-12-10T10:00:00",
        "RequestDate": "2024-12-11T09:00:00",
        "ShipmentDate": "2024-12-12T15:00:00",
        "ShipmentType": "Express",
        "shipment_status": "UPDATED",
        "Notes": "Handle with care. Fragile items.",
        "CarrierCode": "DHL123",
        "CarrierDescription": "DHL Express",
        "ServiceCode": "EXP001",
        "PaymentType": "Credit Card",
        "TransferMode": "Air",
        "TotalPackageCount": 3,
        "TotalPackageWeight": 12.5
    }

    # PUT request to update shipment
    put_response = requests.put(f"{base_url}/{shipment_id}", json=updated_shipment, headers=headers)
    assert put_response.status_code == 200  # Expecting no content after PUT
    
    # Check if the PUT response has a body (it shouldn't)
    if put_response.content:
        response_data = put_response.json()  # Parse JSON response if body is not empty
        assert response_data["shipment_status"] == "UPDATED"
    else:
        print("PUT request returned 200 but no response body.")

    requests.delete(f"{base_url}/{id}", headers=headers)

def test_link_orders(base_url):
    # Link orders to shipment
    url_post = base_url + "/1/link-orders"
    link_payload = {"OrderIds": [1, 2]}
    post_response = requests.post(url_post, json=link_payload, headers=headers)
    assert post_response.status_code == 200, f"Failed to link orders: {post_response.text}"

    # Retrieve linked orders
    get_response = requests.get(base_url + "/1/orders", headers=headers)
    assert get_response.status_code == 200, f"Failed to fetch linked orders: {get_response.text}"

    # Disconnect orders from shipment
    disconnect_url = base_url + "/1/disconnect-orders"
    disconnect_payload = {"OrderIds": [1, 2]}
    disconnect_response = requests.post(disconnect_url, json=disconnect_payload, headers=headers)
    assert disconnect_response.status_code == 200, f"Failed to disconnect orders: {disconnect_response.text}"

    # Verify orders are disconnected
    get_response_after_disconnect = requests.get(base_url + "/1/orders", headers=headers)
    assert get_response_after_disconnect.status_code == 200, f"Failed to fetch linked orders after disconnect: {get_response_after_disconnect.text}"
    linked_orders_after_disconnect = get_response_after_disconnect.json()
    assert len(linked_orders_after_disconnect) == 0, "Orders were not successfully disconnected"