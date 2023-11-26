import {baseUrl, LS} from "../Consts";

export const getBillboardTypesEndpoint = `${baseUrl}/BillboardTypes`;

export function GetBillboardTypes()
{
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
    return fetch(getBillboardTypesEndpoint, requestInfo);
}