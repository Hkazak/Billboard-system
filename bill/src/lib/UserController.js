import { baseUrl } from "./Consts";

export const userSignUpEndpoint = `${baseUrl}/Users/sign-up`;
export const userSignInEndpoint = `${baseUrl}/Users/sign-in`;

export async function RegisterUser(userName, userEmail, userPassword, userConfirmPassword){
    let body = {
        'name': userName,
        'email': userEmail,
        'password': userPassword,
        'confirmPassword': userConfirmPassword
    };

    console.log(body);

    const response = await fetch(userSignUpEndpoint, {
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

export async function AuthorizeUser(userEmail, userPassword){
    let body = {
        'email': userEmail,
        'password': userPassword
    };

    console.log(body);

    const response = await fetch(userSignInEndpoint, {
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