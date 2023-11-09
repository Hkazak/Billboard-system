import { baseUrl } from "../Consts";

export const managerSignInEndpoint = `${baseUrl}/Managers/sign-in`;
export const userResetPasswordSendEmailEndpoint = `${baseUrl}/Users/password/forgot`;
export const userResetPasswordChangePasswordEndpoint = `${baseUrl}/Users/password/reset`;

export async function AuthorizeManager(userEmail, userPassword){
    let body = {
        'email': userEmail,
        'password': userPassword
    };

    console.log(body);

    const response = await fetch(managerSignInEndpoint, {
        method: 'POST',
        headers: {
            "Access-Control-Allow-Origin": "*",
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(body)
    });

    return response;
}