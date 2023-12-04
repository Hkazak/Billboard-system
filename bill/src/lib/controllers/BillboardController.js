import {LS, baseUrl} from "../Consts";

export const createBillboardEndpoint = `${baseUrl}/Billboards`;
export const getBillboardListEndpoint = `${baseUrl}/Billboards`;
export const getShortBillboardListEndpoint = `${baseUrl}/Billboards/short`;

export async function CreateBillboardRequest(name, address, description, groupOfTariffs, billboardType, billboardSurfaceId, penalty, height, width, pictures) {
    let body = {
        "name": name,
        "address": address,
        "description": description,
        "groupOfTariffs": groupOfTariffs,
        "billboardType": billboardType,
        "billboardSurfaceId": billboardSurfaceId,
        "penalty": penalty,
        "height": height,
        "width": width,
        "pictures": pictures.map(e => e.data.split(',')[1])
    };
    const accessToken = localStorage.getItem(LS.accessToken);

    const requestInfo = {
        method: 'POST',
        headers: {
            "Access-Control-Allow-Origin": "*",
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': `bearer ${accessToken}`
        },
        body: JSON.stringify(body)
    };

    const response = await fetch(createBillboardEndpoint, requestInfo);

    return response;
}

export function GetBillboardList() {
    const accessToken = localStorage.getItem(LS.accessToken);

    return fetch(getBillboardListEndpoint, {
        method: 'GET',
        headers: {
            "Access-Control-Allow-Origin": "*",
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': `bearer ${accessToken}`
        }
    });
}

export function GetShortBillboardList() {
    const accessToken = localStorage.getItem(LS.accessToken);
    const requestInfo = {
        method: 'GET',
        headers: {
            "Access-Control-Allow-Origin": "*",
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': `bearer ${accessToken}`
        }
    };
    return fetch(getShortBillboardListEndpoint, requestInfo);
}