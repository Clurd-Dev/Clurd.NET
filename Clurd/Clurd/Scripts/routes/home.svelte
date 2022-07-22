<script>
    import { onMount } from "svelte";
    import axios from 'axios';
    import qs from 'qs';
    import 'bootstrap/dist/css/bootstrap.css';
    import Fa from 'svelte-fa/src/fa.svelte';
    import { faFile, faFolder } from '@fortawesome/free-solid-svg-icons/index.es';
    import { dialogs } from "svelte-dialogs";
    import Info from '$lib/components/Info.svelte';
    import { Circle2 } from 'svelte-loading-spinners'
    let files = [];
    let loading = true;
    onMount(() => {
        var data = qs.stringify({
            'path': './' 
        });
        var config = {
            method: 'post',
            url: '/api/files',
            headers: { 
                'Content-Type': 'application/x-www-form-urlencoded'
            },
            data : data
            };

        axios(config)
            .then(function (response) {
                files = response.data;
                loading = false;
        })
            .catch(function (error) {
                console.log(error);
        });
    });
</script>

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
<div class="grid-container">
    {#each files as file}
    {#if file.Dir == false}
        <div class="grid-item" on:click={() => dialogs.modal(Info, {
            file: file.Name,
            path: file.FullPath,
            size: file.Size,
            lastaccess: file.LastAccess,
            lastwrite: file.LastWrite,
            creation: file.Creation,
            readonly: file.ReadOnlyFil
        })}>
            <Fa icon={faFile} size="1.5x"></Fa>   
            <p>{file.Name}</p>
        </div>
            {:else}
            <div class="grid-item">
                <Fa icon={faFolder} size="1.5x"></Fa>   
                <p>{file.Name}</p>
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