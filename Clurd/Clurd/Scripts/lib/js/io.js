import qs from 'qs';
import axios from 'axios';
export async function getfiles(path){
    console.log(path.path);
    let data = qs.stringify({
        'path': path,
        'username': sessionStorage.getItem("username"),
        'password': sessionStorage.getItem("password")
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
        'new': newpath ,
        'username': sessionStorage.getItem("username"),
        'password': sessionStorage.getItem("password")
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
        'path': path,
        'username': sessionStorage.getItem("username"),
        'password': sessionStorage.getItem("password")
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

export async function Copy(oldpath, newpath){
  let data = qs.stringify({
    'oldpath': oldpath,
    'newpath': newpath,
    'username': sessionStorage.getItem("username"),
    'password': sessionStorage.getItem("password")
  });
  let config = {
      method: 'post',
      url: '/api/copy',
      headers: { 
        'Content-Type': 'application/x-www-form-urlencoded'
      },
      data : data
    };
  return  await (await axios(config)).data;
}

export async function Move(oldpath, newpath){
  let data = qs.stringify({
    'oldpath': oldpath,
    'newpath': newpath,
    'username': sessionStorage.getItem("username"),
    'password': sessionStorage.getItem("password")
  });
  let config = {
      method: 'post',
      url: '/api/move',
      headers: { 
        'Content-Type': 'application/x-www-form-urlencoded'
      },
      data : data
    };
  return  await (await axios(config)).data;
}

export async function Upload(input, path){
  const formData = new FormData();
  console.log(input.files);
  for(let i = 0; i < input.files.length; i++){
    formData.append(input.files[i].name, input.files[i]);
  }

  return await (await axios({
    method: "POST",
    url: "/api/upload/?path=" + path + "&username=" + sessionStorage.getItem("username") + "&password=" + sessionStorage.getItem("password"),
    data: formData,
    headers: {
      "Content-Type": "multipart/form-data"
    }
  })).data
}