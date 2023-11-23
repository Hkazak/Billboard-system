import { LS, baseUrl } from "../Consts";

export const createBillboardEndpoint = `${baseUrl}/Billboards`;
export const getBillboardListEndpoint = `${baseUrl}/Billboards`;

export async function CreateBillboard(name, address, description, groupOfTariffs, billboardType, billboardSurfaceId, penalty, height, width, pictureSource) {
    let body = {
        "name": name,
        "address": address,
        "description": description,
        "groupOfTariffs": groupOfTariffs,
        "billboardType": "SingleSide",
        "billboardSurfaceId": billboardSurfaceId,
        "penalty": penalty,
        "height": height,
        "width": width,
        "pictureSource": pictureSource
    };
    const accessToken = localStorage.getItem(LS.accessToken);

    console.log(body);

    const response = await fetch(createBillboardEndpoint, {
        method: 'POST',
        headers: {
            "Access-Control-Allow-Origin": "*",
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': `bearer ${accessToken}`
        },
        body: JSON.stringify(body)
    });

    return response;
}

export async function GetBillboardList(name, address, description, groupOfTariffs, billboardType, billboardSurfaceId, penalty, height, width, pictureSource) {
    const accessToken = localStorage.getItem(LS.accessToken);

    const response = await fetch(getBillboardListEndpoint, {
        method: 'GET',
        headers: {
            "Access-Control-Allow-Origin": "*",
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': `bearer ${accessToken}`
        }
    });

    return response;
}