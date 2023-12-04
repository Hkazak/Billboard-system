import {baseUrl, LS} from "../Consts";

export const getGroupOfTariffsListEndpoint = `${baseUrl}/GroupOfTariffs`;
export const getBillboardSurfacesListEndpoint = `${baseUrl}/BillboardSurface`;

export async function GetGroupOfTariffs() {
    const accessToken = localStorage.getItem(LS.accessToken);
    const response = await fetch(getGroupOfTariffsListEndpoint, {
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

export async function getBillboardSurfacesList() {
    const accessToken = localStorage.getItem(LS.accessToken);
    return await fetch(getBillboardSurfacesListEndpoint, {
        method: 'GET',
        headers: {
            "Access-Control-Allow-Origin": "*",
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': `bearer ${accessToken}`
        }
    });
}