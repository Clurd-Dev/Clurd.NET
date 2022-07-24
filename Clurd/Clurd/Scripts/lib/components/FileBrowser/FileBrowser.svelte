<script>
    import axios from 'axios';
    import { DialogContent, dialogs, getClose } from 'svelte-dialogs';
    import { onMount } from 'svelte';
    import { getfiles, Copy, Move } from '$lib/js/io.js';
    import Fa from 'svelte-fa/src/fa.svelte';
    import { faFile, faFolder, faArrowLeftLong } from '@fortawesome/free-solid-svg-icons/index.es';
    let current_path, files = [];
    export let old_path, current_file;
    onMount(async() => {
        current_path = await (await axios.get("/api/config")).data;
        files = await getfiles(current_path);
    });
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
    async function copy(){
        if(Copy(old_path, current_path + "/" + current_file)){
            dialogs.alert("File copied successfully").then(() => location.reload());
        }else{
            dialogs.alert("Error during coping of file").then(() => getClose());
        }
    }
    async function move(){
        if(Move(old_path, current_path + "/" + current_file)){
            dialogs.alert("File moved successfully").then(() => location.reload());
        }else{
            dialogs.alert("Error during moving of file").then(() => getClose());
        } 
    }
</script>

<DialogContent>
    <h3 slot="header">Select the path where you want to move file</h3>
    

    <svelte:fragment slot="body">
        <ul class="nav">
            <li class="nav-item">
                <a class="nav-link" href="#" on:click={goback}>
                    <Fa icon={faArrowLeftLong} size="2x"></Fa>
                </a>
            </li>
        </ul>
        <ul class="list-group">
            {#each files as file}
                {#if file.Dir == true}
                    <li class="list-group-item" on:click={() => navigatetofolder(file.FullPath)}>{file.Name}</li>
                {/if}
            {/each}
        </ul>
        <br/>
        <button class="btn btn-primary" on:click={copy}>Copy file here</button>
        <button class="btn btn-primary" on:click={move}>Move file here</button>
    </svelte:fragment>
</DialogContent>