// will contain functions, that make requess to the API using axios
import axios from 'axios';

//IP kan skifte, husk at check ipconfig
const apiURL = "http://192.168.1.70:7076/";

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
        console.log(session)
        const response = await axios.get(`${apiURL}api/User/${session.userId}/?sessionId=${session.sessionId}`);
        console.log(response)
        if (response.data == "" || response.data == null) {
            console.log(response.data == "")
            return null;
        }
        return response.data;
    } catch (error) {
        console.error('Error fetching data from API: ', error);
        return null;
    }
    console.log(response);
};

export const postFridgeData = async (FridgeName) => {
    try {
    const response = await axios({
        method: 'post',
        url: `${apiURL}api/Fridge`,
        data: {
            fridgeName: FridgeName
        }
    });
    return response.data;
    } catch (error) {
        console.error('Error posting data to API: ', error);
        return null;
    }
};

export const postProductData = async (FridgeId, ProductName, ExpiryDate, CategoryId) => {
    try {
    const response = await axios({
        method: 'post',
        url: `${apiURL}api/Product`,
        data: {
            fridgeId: FridgeId,
            productName: ProductName,
            expiryDate: ExpiryDate,
            categoryId: CategoryId,
        }
    });
    return response.data;
    } catch (error) {
        console.error('Error posting data to API: ', error);
        return null
    }
}

export const getUserFridgeData = async (userId) => {
    try {
        const response = await axios.get(`${apiURL}api/UserFridge/user/${userId}`);
        console.log(response)
        if (response.data == "" || response.data == null) {
            console.log(response.data == "", " there is no fridge to see")
            return null;
        }
        return response.data
    } catch (error) {
        console.error('Error getting data: ', error);
        return null;
    }
};

export const getFridgeCategoryData = async (fridgeId) => {
    try {
        const response = await axios.get(`${apiURL}api/Category/Fridge/${fridgeId}`);
        console.log(response)
        if(response.data == "" || response.data == null) {
            console.log(response.data == "", " there is no categories")
            return null;
        }
        return response.data
    } catch (error) {
        console.error('Error getting data: ', error);
        return null;
    }
};

export const postUserFridgeData = async (UserId, FridgeId) => {
    try {
        const response = await axios({
            method: 'post',
            url: `${apiURL}api/UserFridge`,
            data: {
                userId: UserId,
                fridgeId: FridgeId
            }
        });
        return response.data;
    } catch (error) {
        console.error('Error posting data to API: ', error);
        return null;
    }
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
};

export async function logoutUser() {    
    const response = await axios({
        method: 'post',
        url: `${apiURL}api/User/logout`,
        data: {
            userId: session.userId
        }

    }).catch(error=>{
        console.error('Error posting data to API: ', error);
        return;
    })
    
};

export function getSession() {
    return session;
}

export async function deleteProductData(productId) {
    try {
        const response = await axios.delete(`${apiURL}api/Product/${productId}`);
        
        // Check if the response status is 204 (NoContent)
        if (response.status === 204) {
          return true; // Product deleted successfully
        } else {
          console.error('Error deleting product:', response.statusText);
          return false; // Product deletion failed
        }
      } catch (error) {
        console.error('Error deleting product:', error.message);
        return false; // Product deletion failed
      }
  }