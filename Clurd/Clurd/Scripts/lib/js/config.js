import axios from "axios";
import qs from 'qs';
import md5 from 'md5';
export async function Getconfig(){
    return await (await axios.get("/api/config")).data
}

export async function Updateconfig(path, username, password, oldpass){
    if(oldpass != password){
        password = md5(password);
    }
    var data = qs.stringify({
        'username': username,
        'password': password,
        'path': path
      });
      var config = {
        method: 'post',
        url: '/api/config',
        headers: { 
          'Content-Type': 'application/x-www-form-urlencoded'
        },
        data : data
      };
      
      return await (await axios(config)).data;  
}