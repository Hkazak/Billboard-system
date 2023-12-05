import {baseUrl, LS} from "../Consts";

export const GetBookedOrdersEndpoint = `${baseUrl}/Orders/booked`;
export const CreateOrderEndpoint = `${baseUrl}/Orders`;
export const GetOrdersListEndpoint = `${baseUrl}/Orders`;
export const CalculatePriceEndpoint = `${baseUrl}/Orders/price`;
export const GetOrdersStatusesEndpoint = `${baseUrl}/Orders/statuses`
export const BaseControllerEndpoint = `${baseUrl}/Orders`;

export function GetBookedOrdersRequest(billboardId, tariffId = null) {
    let endpoint = `${GetBookedOrdersEndpoint}?billboardId=${billboardId}`;
    if (tariffId !== null) {
        endpoint = `${endpoint}&tariffId=${tariffId}`;
    }
    const accessToken = localStorage.getItem(LS.accessToken);
    const requestInfo = {
        method: 'GET',
        headers: {
            "Access-Control-Allow-Origin": "*",
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': `bearer ${accessToken}`
        }
    }
    return fetch(endpoint, requestInfo);
}

export function CreateOrderRequest(billboardId, startDate, endDate, tariffId, files) {
    const accessToken = localStorage.getItem(LS.accessToken);
    const startDateString = startDate.toLocaleDateString('ru-RU')
        .replaceAll('/', '-')
        .replaceAll('.', '-');
    const endDateString = endDate.toLocaleDateString('ru-RU')
        .replaceAll('/', '-')
        .replaceAll('.', '-');
    const requestBody = {
        billboardId: billboardId,
        tariffId: tariffId,
        startDate: startDateString,
        endDate: endDateString,
        files: files.map(e=>e.data.split(',')[1])
    };
    const requestInfo = {
        method: 'POST',
        headers: {
            "Access-Control-Allow-Origin": "*",
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': `bearer ${accessToken}`
        },
        body: JSON.stringify(requestBody)
    }
    return fetch(CreateOrderEndpoint, requestInfo);
}

export function CalculatePriceRequest(billboardId, startDate, endDate, tariffId) {
    const accessToken = localStorage.getItem(LS.accessToken);
    const startDateString = startDate.toLocaleDateString('ru-RU')
        .replaceAll('/', '-')
        .replaceAll('.', '-');
    const endDateString = endDate.toLocaleDateString('ru-RU')
        .replaceAll('/', '-')
        .replaceAll('.', '-');
    const requestBody = {
        billboardId: billboardId,
        tariffId: tariffId,
        startDate: startDateString,
        endDate: endDateString
    };
    const requestInfo = {
        method: 'POST',
        headers: {
            "Access-Control-Allow-Origin": "*",
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': `bearer ${accessToken}`
        },
        body: JSON.stringify(requestBody)
    }
    return fetch(CalculatePriceEndpoint, requestInfo);
}

export function GetOrdersStatusesRequest() {
    const accessToken = localStorage.getItem(LS.accessToken);
    const requestInfo = {
        method: 'GET',
        headers: {
            "Access-Control-Allow-Origin": "*",
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': `bearer ${accessToken}`
        }
    }
    return fetch(GetOrdersStatusesEndpoint, requestInfo);
}

export function GetOrdersListRequest() {
    const accessToken = localStorage.getItem(LS.accessToken);
    const requestInfo = {
        method: 'GET',
        headers: {
            "Access-Control-Allow-Origin": "*",
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': `bearer ${accessToken}`
        }
    }
    return fetch(GetOrdersListEndpoint, requestInfo);
}

export function ApproveOrderRequest(orderId)
{
    const accessToken = localStorage.getItem(LS.accessToken);
    const endpoint = `${BaseControllerEndpoint}/${orderId}/approve`;
    const requestInfo = {
        method: 'PUT',
        headers: {
            "Access-Control-Allow-Origin": "*",
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': `bearer ${accessToken}`
        }
    }
    return fetch(endpoint, requestInfo);
}

export function CancelOrderRequest(orderId)
{
    const accessToken = localStorage.getItem(LS.accessToken);
    const endpoint = `${BaseControllerEndpoint}/${orderId}/cancel`;
    const requestInfo = {
        method: 'PUT',
        headers: {
            "Access-Control-Allow-Origin": "*",
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': `bearer ${accessToken}`
        }
    }
    return fetch(endpoint, requestInfo);
}

export function RecalculatePriceRequest(orderId, startDate, endDate)
{
    const accessToken = localStorage.getItem(LS.accessToken);
    const startDateString = startDate.toLocaleDateString('ru-RU')
        .replaceAll('/', '-')
        .replaceAll('.', '-');
    const endDateString = endDate.toLocaleDateString('ru-RU')
        .replaceAll('/', '-')
        .replaceAll('.', '-');
    const endpoint = `${baseUrl}/Orders/${orderId}/price`;
    const requestBody = {
        startDate: startDateString,
        endDate: endDateString
    };
    const requestInfo = {
        method: 'POST',
        headers: {
            "Access-Control-Allow-Origin": "*",
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': `bearer ${accessToken}`
        },
        body: JSON.stringify(requestBody)
    }
    return fetch(endpoint, requestInfo);
}

export function ChangePriceRequest(orderId, startDate, endDate)
{
    const accessToken = localStorage.getItem(LS.accessToken);
    const startDateString = startDate.toLocaleDateString('ru-RU')
        .replaceAll('/', '-')
        .replaceAll('.', '-');
    const endDateString = endDate.toLocaleDateString('ru-RU')
        .replaceAll('/', '-')
        .replaceAll('.', '-');
    const endpoint = `${baseUrl}/Orders/${orderId}`;
    const requestBody = {
        startDate: startDateString,
        endDate: endDateString
    };
    const requestInfo = {
        method: 'PUT',
        headers: {
            "Access-Control-Allow-Origin": "*",
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': `bearer ${accessToken}`
        },
        body: JSON.stringify(requestBody)
    }
    return fetch(endpoint, requestInfo);
}