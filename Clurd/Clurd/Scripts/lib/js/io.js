import qs from 'qs';
import axios from 'axios';
export async function getfiles(path){
    console.log(path);
    let data = qs.stringify({
        'path': path
    });
    let config = {
        method: 'post',
        url: '/api/files',
        headers: { 
            'Content-Type': 'application/x-www-form-urlencoded'
        },
        data : data
        };

    try {
        return await (await axios(config)).data;
    } catch (error) {
       console.error(error);
       return "0"; 
    }   
}

export async function Rename(old, newpath){
    let data = qs.stringify({
        'old': old,
        'new': newpath 
      });
    let config = {
        method: 'post',
        url: '/api/rename',
        headers: { 
          'Content-Type': 'application/x-www-form-urlencoded'
        },
        data : data
      };
    return  await (await axios(config)).data;
}

export async function Delete(path){
    let data = qs.stringify({
        'path': path
    });
    let config = {
        method: 'post',
        url: '/api/delete',
        headers: { 
          'Content-Type': 'application/x-www-form-urlencoded'
        },
        data : data
      };
    return  await (await axios(config)).data;
}