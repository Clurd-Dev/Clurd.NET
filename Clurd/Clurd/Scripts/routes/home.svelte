<script>
    import { SvelteToast,toast } from '@zerodevx/svelte-toast'
    import { onMount } from "svelte";
    import axios from 'axios';
    import 'bootstrap/dist/css/bootstrap.css';
    import Fa from 'svelte-fa/src/fa.svelte';
    import { faFile, faFolder, faArrowLeftLong } from '@fortawesome/free-solid-svg-icons/index.es';
    import { dialogs } from "svelte-dialogs";
    import Info from '$lib/components/Info.svelte';
    import { Circle2 } from 'svelte-loading-spinners';
    import Contex from '$lib/components/contex/contex.svelte';
    import { rightClick, hideMenu } from '$lib/js/menu';
    import { getfiles, Upload } from '$lib/js/io.js';
    let only_file, current_file, location_website, path, pathsplitted, current_path, inputfiles;
    let files = [];
    let loading = true;
    onMount(async() => {
        current_path = await (await axios.get("/api/config")).data;
        location_website = location.origin;
        document.onclick = hideMenu;
        files = await getfiles(current_path);
        loading = false;
    });
    function contex(e) {
        if(rightClick(e) != "0"){
            path = rightClick(e);
            pathsplitted = path.split("/")
            current_file = pathsplitted[pathsplitted.length - 1];
        }
	}

    async function navigatetofolder(path){
        current_path = path;
        files = await getfiles(path);
    }

    async function goback(){
        let backup = current_path;
        let pathsplit = current_path.split("/");
        pathsplit.pop();
        current_path = pathsplit.join("/");
        let newfiles = await getfiles(current_path);
        if(newfiles == "0"){
            dialogs.alert("Can't go back through home");
            current_path = backup;
        }else{
            files = newfiles;
        }
    }

    async function upload(){
      toast.push("Upload started, please wait");
      
      if(await Upload(inputfiles, current_path.split("/").join("|"))){
        toast.pop();
        toast.push("Upload completed!");
      }else{
        toast.pop();
        toast.push("Error during the uploading of file");
      };
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

<Contex {current_file} {path} {pathsplitted}/>
<SvelteToast />
<ul class="nav">
    <li class="nav-item">
        <a class="nav-link" href="" on:click={goback}>
            <Fa icon={faArrowLeftLong} size="2x"></Fa>
        </a>
    </li>
  </ul>

{#if loading == true}
<center>
    <Circle2 size="256"  unit="px"></Circle2>
    <br>
    <br>
    <p>Getting files information from server</p>
</center>
{:else}
<div class="grid-container" on:contextmenu={contex}>
    {#each files as file}
    {#if file.Dir == false}
        <div class="grid-item" id={file.FullPath} on:click={() => dialogs.modal(Info, {
            file: file.Name,
            path: file.FullPath,
            size: file.Size,
            lastaccess: file.LastAccess,
            lastwrite: file.LastWrite,
            creation: file.Creation,
            readonly: file.ReadOnlyFil
        })}>
        <div id={file.FullPath}>
            <Fa icon={faFile} size="1.5x" id={file.FullPath}></Fa>   
        </div>
            
            <p id={file.FullPath}>{file.Name}</p>
        </div>
            {:else}
            <div class="grid-item" id={file.FullPath} on:click={() => navigatetofolder(file.FullPath)} >
                <div id={file.FullPath}>
                    <Fa icon={faFolder} size="1.5x" ></Fa>  
                </div>
                 
                <p id={file.FullPath}>{file.Name}</p>
            </div>
            {/if}
         
    {/each}
  </div>
  <br/>
  <center>
    <h3>Upload files in this directory</h3>
  </center>
  <br/>

    <div class="form-group" style="margin: 20px;">
       <input type="file" class="form-control" id="files" name="image" multiple bind:this={inputfiles}/>
    </div>

    <br/>
    <div class="form-group" align="center">
       <button type="submit" class="btn btn-primary" on:click={upload}>Upload</button>
    </div>
    <br/>


  {/if}


<style>
.grid-container {
  display: grid;
  grid-template-columns: auto auto auto;
  padding: 10px;
}
.grid-item {
  background-color: rgba(255, 255, 255, 0.8);
  border: 1px solid rgba(0, 0, 0, 0.8);
  padding: 20px;
  font-size: 30px;
  text-align: center;
}
</style>