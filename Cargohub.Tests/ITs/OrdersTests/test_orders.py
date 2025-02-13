import pytest
import requests


@pytest.fixture
def base_url():
    return 'http://localhost:5000/api/order'


headers = {
    "Accept": "application/json",
    "User-Agent": "value",
    "API_key": "a1b2c3d4",
}

# Test adding and removing an order
def test_add_remove_order(base_url):
    new_order = {
        "source_id": 33,
        "order_date": "2019-04-03T11:33:15Z",
        "request_date": "2019-04-07T11:33:15Z",
        "reference": "ORDER123",
        "reference_extra": "Extra details",
        "order_status": "Delivered",
        "notes": "Some notes",
        "shipping_notes": "Some shipping notes",
        "picking_notes": "Some picking notes",
        "warehouse_id": 18,
        "ship_to": "123 Main St, Springfield",
        "bill_to": "456 Elm St, Shelbyville",
        "shipment_id": 1,
        "total_amount": 1000.50,
        "total_discount": 50.75,
        "total_tax": 150.25,
        "total_surcharge": 20.00,
    }

    # POST request to add order
    post_response = requests.post(base_url, json=new_order, headers=headers)
    assert post_response.status_code == 201, f"Expected 201 Created, got {post_response.status_code}"
    order_id = post_response.json().get("id")
    assert order_id is not None, "Order ID should not be None"

    # GET request to retrieve the order
    get_response = requests.get(f"{base_url}/{order_id}", headers=headers)
    assert get_response.status_code == 200, f"Expected 200 OK, got {get_response.status_code}"
    response_data = get_response.json()
    assert response_data["reference"] == "ORDER123", "Order reference mismatch"

    # DELETE request to remove the order
    del_response = requests.delete(f"{base_url}/{order_id}", headers=headers)
    assert del_response.status_code == 204, f"Expected 204 No Content, got {del_response.status_code}"


# Test updating an order
def test_update_order(base_url):
    new_order = {
        "source_id": 33,
        "order_date": "2019-04-03T11:33:15Z",
        "request_date": "2019-04-07T11:33:15Z",
        "reference": "ORDER123",
        "reference_extra": "Extra details",
        "order_status": "Pending",
        "notes": "Initial notes",
        "shipping_notes": "Initial shipping notes",
        "picking_notes": "Initial picking notes",
        "warehouse_id": 18,
        "ship_to": "123 Main St, Springfield",
        "bill_to": "456 Elm St, Shelbyville",
        "shipment_id": 1,
        "total_amount": 1000.50,
        "total_discount": 50.75,
        "total_tax": 150.25,
        "total_surcharge": 20.00,
    }

    # POST request to add order
    post_response = requests.post(base_url, json=new_order, headers=headers)
    assert post_response.status_code == 201
    order_id = post_response.json()["id"]

    # Update the order
    updated_order = {
        **new_order,
        "id": order_id,
        "order_status": "Shipped",
        "notes": "Updated notes",
    }
    put_response = requests.put(f"{base_url}/{order_id}", json=updated_order, headers=headers)
    assert put_response.status_code == 200, f"Expected 200 OK, got {put_response.status_code}"

    # Verify the update
    get_response = requests.get(f"{base_url}/{order_id}", headers=headers)
    assert get_response.status_code == 200
    response_data = get_response.json()
    assert response_data["order_status"] == "Shipped", "Order status was not updated"
    assert response_data["notes"] == "Updated notes", "Order notes were not updated"

    # Cleanup
    requests.delete(f"{base_url}/{order_id}", headers=headers)

# Test fetching all orders
def test_fetch_all_orders(base_url):
    # Send a GET request to fetch all orders
    response = requests.get(base_url+"/amount/10", headers=headers)

    # Verify the status code is 200 (OK)
    assert response.status_code == 200, f"Unexpected status code: {response.status_code}"

    # Check if the response is a list of orders
    orders = response.json()
    assert isinstance(orders, list), "Expected a list of orders"


# Test deleting a non-existent order
def test_delete_nonexistent_order(base_url):
    nonexistent_id = 99999
    response = requests.delete(f"{base_url}/{nonexistent_id}", headers=headers)

    # Verify the status code is 404 (Not Found)
    assert response.status_code == 404, f"Expected 404 Not Found, got {response.status_code}"

def test_link_shipments(base_url):
    # Link orders to shipment
    url_post = base_url + "/1/link-shipments"
    link_payload = {"ShipmentIds": [1, 2]}
    post_response = requests.post(url_post, json=link_payload, headers=headers)
    assert post_response.status_code == 200, f"Failed to link orders: {post_response.text}"

    # Retrieve linked orders
    get_response = requests.get(base_url + "/1/shipments", headers=headers)
    assert get_response.status_code == 200, f"Failed to fetch linked orders: {get_response.text}"

    # Disconnect orders from shipment
    disconnect_url = base_url + "/1/disconnect-shipments"
    disconnect_payload = {"ShipmentIds": [1, 2]}
    disconnect_response = requests.post(disconnect_url, json=disconnect_payload, headers=headers)
    assert disconnect_response.status_code == 200, f"Failed to disconnect orders: {disconnect_response.text}"

    # Verify orders are disconnected
    get_response_after_disconnect = requests.get(base_url + "/1/shipments", headers=headers)
    assert get_response_after_disconnect.status_code == 200, f"Failed to fetch linked orders after disconnect: {get_response_after_disconnect.text}"
    linked_orders_after_disconnect = get_response_after_disconnect.json()
    assert len(linked_orders_after_disconnect) == 0, "Orders were not successfully disconnected"
