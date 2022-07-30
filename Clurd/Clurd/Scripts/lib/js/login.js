import axios from 'axios';
import qs from 'qs';

export async function login(username, password){
    var data = qs.stringify({
        'user': username,
        'password': password
      });
      var config = {
        method: 'post',
        url: '/api/login',
        headers: { 
          'Content-Type': 'application/x-www-form-urlencoded'
        },
        data : data
      };
      
      return await (await axios(config)).data
}

export function logout(){
    sessionStorage.clear();
    location.href = "/login";
}