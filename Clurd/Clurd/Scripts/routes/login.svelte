<script>
    import 'bootstrap/dist/css/bootstrap.css';
    import { login } from '$lib/js/login.js';
    import { onMount } from 'svelte';
    import { dialogs } from 'svelte-dialogs';
    import md5 from 'md5';
    let username, password;
    onMount(async() => {
        await import("bootstrap/dist/js/bootstrap.js");
    });
    async function loginbtn(){
        if(await login(username, md5(password))){
            location.href = "/home";
            sessionStorage.setItem("username", username);
            sessionStorage.setItem("password", md5(password));
        }else{
            dialogs.alert("Wrong password or username");
        }
    }
</script> 
<div class="d-flex justify-content-center logo">
 <img src="/icon.png" alt="logo"/>   
</div>   


<div class="formlogin">
    <div class="mb-3">
      <label for="exampleInputEmail1" class="form-label">Username</label>
      <input type="email" class="form-control" id="exampleInputEmail1" aria-describedby="emailHelp" placeholder="Username" bind:value={username}>
    </div>
    <div class="mb-3">
      <label for="exampleInputPassword1" class="form-label">Password</label>
      <input type="password" class="form-control" id="exampleInputPassword1" placeholder="Password" bind:value={password}>
    </div>
    <div class="d-flex justify-content-center">
        <button type="submit" class="btn btn-primary" on:click={loginbtn}>Login</button>
    </div>
</div>

<style>
    .formlogin{
        padding-left: 150px;
        padding-right: 150px;
        padding-top: 40px;
    }
    .logo{
        margin-top: 50px;
    }
</style>