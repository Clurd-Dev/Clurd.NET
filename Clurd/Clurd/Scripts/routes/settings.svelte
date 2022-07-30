<script>
    import { onMount } from 'svelte';
    import { getinfo } from '$lib/js/systeminfo.js';
    import { Circle2 } from 'svelte-loading-spinners';
    import 'bootstrap/dist/css/bootstrap.css';
    import { Getconfig, Updateconfig } from '$lib/js/config.js';
    import { dialogs } from 'svelte-dialogs';
    import Fa from 'svelte-fa/src/fa.svelte';
    import { faKey } from '@fortawesome/free-solid-svg-icons/index.es';
    let information, loading = true, configuration, path, username, password, showpwd = false, oldpass;
    onMount(async() => {
        configuration = await Getconfig();
        information = await getinfo();
        oldpass = configuration.password;
        console.log(information);
        loading = false;
    });

    async function updateconfig(){
      if(await Updateconfig(document.getElementById("path").value, document.getElementById("username").value, document.getElementById("password").value, oldpass)){
        dialogs.alert("Configuration file updated successfully").then(() => location.href = "/login");
      }else{
        dialogs.alert("Error during update of configuration file");
      };
    }

    function show(){
      console.log("OK");
      if(!showpwd){
        showpwd = !showpwd;
        document.getElementById("password").type = "text";
      }else{
        showpwd = !showpwd;
        document.getElementById("password").type = "password";
      }
    }
</script>
<nav class="navbar navbar-expand-lg bg-light">
    <div class="container-fluid">
      <a class="navbar-brand" href="/home">Clurd.NET</a>
      <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
        <span class="navbar-toggler-icon"></span>
      </button>
      <div class="collapse navbar-collapse" id="navbarSupportedContent">
        <ul class="navbar-nav me-auto mb-2 mb-lg-0">
          <li class="nav-item">
            <a class="nav-link" href="/settings">Settings</a>
          </li>
        </ul>
      </div>
    </div>
  </nav>
  <br/>
  <br/>
{#if loading == true}
<center>
    <br>
    <br>
    <Circle2 size="256"  unit="px"></Circle2>
    <br>
    <br>
    <p>Getting information from server</p>
</center>
{:else}
<div class="info">
        <div class="mb-3">
        <label for="formGroupExampleInput" class="form-label">CPU Model</label>
        <input type="text" class="form-control" id="formGroupExampleInput" value={information.Cpus[0].Name} readonly>
    </div>
    <div class="mb-3">
        <label for="formGroupExampleInput2" class="form-label">Total physical memory</label>
        <input type="text" class="form-control" readonly id="formGroupExampleInput2" value={information.MemoryStatus.TotalPhysical / 1000000000 + "Gb"}>
    </div>
    <div class="mb-3">
        <label for="formGroupExampleInput2" class="form-label">Motherboard</label>
        <input type="text" class="form-control" readonly id="formGroupExampleInput2" value={information.Motherboards[0].Manufacturer + " " + information.Motherboards[0].Product + " " + information.Motherboards[0].SerialNumber}>
    </div>
    <div class="d-flex justify-content-center">
      <h4>Configuration file</h4>
    </div>
    <div class="mb-3">
      <label for="formGroupExampleInput2" class="form-label">Path</label>
      <input type="text" class="form-control" id="path" value={configuration.path}>
    </div>
    <div class="mb-3">
      <label for="formGroupExampleInput2" class="form-label">Username</label>
      <input type="text" class="form-control" id="username" value={configuration.username}>
    </div>
    <label for="formGroupExampleInput2" class="form-label">Password</label>

    <div class="mb-3 input-group">
      <input type="password" class="form-control" id="password" value={configuration.password}>
      <span class="input-group-text" id="basic-addon2" on:click={show}><Fa icon={faKey}></Fa></span>
    </div>
    <div class="d-flex justify-content-center">
      <button class="btn btn-warning" on:click={updateconfig}>Update configuration file</button>
    </div>
</div>
{/if}
<style>
    .info{
        margin: 20px;
    }
</style>
