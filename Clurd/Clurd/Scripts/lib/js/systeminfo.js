import axios from 'axios';

export async function getinfo(){
    var config = {
        method: 'get',
        url: '/api/systeminfo'
      };
      
    return await (await axios(config)).data;
}