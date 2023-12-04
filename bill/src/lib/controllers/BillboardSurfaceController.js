import {baseUrl, LS} from "../Consts";

export const CreateBillboardSurfaceEndpoint = `${baseUrl}/BillboardSurface`;

export function CreateBillboardSurfaceRequest(surface) {
    const accessToken = localStorage.getItem(LS.accessToken);
    const requestBody = {
        surface: surface
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
    };
    return fetch(CreateBillboardSurfaceEndpoint, requestInfo);
}