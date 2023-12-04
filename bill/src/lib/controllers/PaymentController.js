import {baseUrl, LS} from "../Consts";

export const CreatePaymentEndpoint = `${baseUrl}/Payments`;

export function CreatePaymentRequest(orderId) {
    const accessToken = localStorage.getItem(LS.accessToken);
    const requestBody = {
        orderId: orderId
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
    return fetch(CreatePaymentEndpoint, requestInfo);
}