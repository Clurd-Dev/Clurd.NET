<script>
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
    import { getfiles } from '$lib/js/io.js';
    let only_file, current_file, location_website, path, pathsplitted, current_path;
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
</script>

<Contex {current_file} {path} {pathsplitted}/>

<ul class="nav">
    <li class="nav-item">
        <a class="nav-link" href="" on:click={goback}>
            <Fa icon={faArrowLeftLong} size="2x"></Fa>
        </a>
    </li>
  </ul>

{#if loading == true}
<center>
    <br>
    <br>
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