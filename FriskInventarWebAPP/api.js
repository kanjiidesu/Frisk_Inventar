// will contain functions, that make requess to the API using axios
import axios from 'axios';

const apiURL = "https://localhost:7076/";
var session = null;
if (localStorage.getItem("session")) {
    session = JSON.parse(localStorage.getItem("session"))
}

export const getFridgeData = async () => {
    try {
        const response = await axios.get(`${apiURL}api/Fridge`);
        console.log(response)
        return response.data;
    } catch (error) {
        console.error('Error fetching data from API: ', error);
        throw error;
    }
    console.log(response);
};

export const getUser = async () => {
    try {
        if (session == null) {
            console.error("Not logged in")
            return null;
        }
        const response = await axios.get(`${apiURL}api/User/${session.userId}/?sessionId=${session.sessionId}`);
        console.log(response)
        return response.data;
    } catch (error) {
        console.error('Error fetching data from API: ', error);
        throw error;
    }
    console.log(response);
};

export const postFridgeData = async (FridgeName) => {
    const response = await axios({
        method: 'post',
        url: `${apiURL}api/Fridge`,
        data: {
            fridgeName: FridgeName
        }
    }).catch(error=>{
        console.error('Error posting data to API: ', error);
    })
    return response.data;
};

export const postUserData = async (UserName, Email, Password) => {
    const response = await axios({
        method: 'post',
        url: `${apiURL}api/User`,
        data: {
            userName: UserName,
            email: Email,
            password: Password
        }
    }).catch(error=>{
        console.error('Error posting data to API: ', error);
        return;
    })
    return response.data;
};

// this endpoint needs to be created, it is not yet
export async function loginUser(UserName, Password) {
    const response = await axios({
        method: 'post',
        url: `${apiURL}api/User/login`,
        data: {
            userName: UserName,
            password: Password
        }
    }).catch(error=>{
        console.error('Error posting data to API: ', error);
        return;
    })
    session = response.data;
    localStorage.setItem("session", JSON.stringify(session))
    return response.data;
    // try {
    //     const response = await axios.post('/api/User/login', { userName, password});
    //     return response.data;
    // } catch (error) {
    //     throw error;
    // }
};

export function getSession() {
    return session;
}