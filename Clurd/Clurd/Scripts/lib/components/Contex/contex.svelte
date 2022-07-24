<script>
	export let current_file, path, pathsplitted;
	let url;
	import { dialogs } from 'svelte-dialogs';
	import { onMount } from 'svelte';
	import { Rename, Delete } from '$lib/js/io.js';
    import Fa from 'svelte-fa/src/fa.svelte';
    import { faCopy } from '@fortawesome/free-solid-svg-icons/index.es';
	import FileBrowser from '$lib/components/FileBrowser/FileBrowser.svelte';
	import './contex.css';
	onMount(()=>{
		url = location.origin;
		console.log(path);
	});

	async function renamefile(){
		console.log("OK");
		let newfile = await dialogs.prompt("Please enter the new filename with extension for " + current_file);
		pathsplitted[pathsplitted.length - 1] = newfile;
		let rsp = await Rename(path, pathsplitted.join("/"));
		if(rsp){
			dialogs.alert("File renamed correctly").then(() => location.reload());
		}else{
			dialogs.alert("Error during renaming of file");
		}
	}
	async function remove(){
		dialogs.confirm("Are you sure to delete this file?").then(response => {
			if(response){
				if(Delete(path)){
					dialogs.alert("File removed successfully").then(() => location.reload());
				}else{
					dialogs.alert("Error during deleting file");
				};
			}
		});
	}
	function downloadfile(){
		console.log(path);
		var myHeaders = new Headers();
		myHeaders.append("Content-Type", "application/x-www-form-urlencoded");

		var urlencoded = new URLSearchParams();
		urlencoded.append("path", path);

		var requestOptions = {
		method: 'POST',
		headers: myHeaders,
		body: urlencoded,
		redirect: 'follow'
		};

		fetch("/api/file", requestOptions)
		.then(function(resp) {
			return resp.blob();
		}).then(function(blob) {
			// @ts-ignore it's loaded in head of page
			return download(blob, current_file);
		})
		.catch(error => console.log('error', error));
		
		// fetch('/api/file', {
		// 	method: 'POST',
		// 	body: JSON.stringify({
		// 		path: path + "/" +current_file
		// 	}),
		// 	headers: {
		// 		'Content-Type': 'application/json'
		// 	},
		// })
	}
</script>

<svelte:head>
	<script src="https://cdnjs.cloudflare.com/ajax/libs/downloadjs/1.4.8/download.min.js"></script>

	<link
		rel="stylesheet"
		href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css"
	/>
</svelte:head>
<div id="contextMenu" class="context-menu" style="display: none">
	<ul class="menu">
		<li class="rename" on:click={renamefile}>
			<a href="#0" ><i class="fa fa-pencil" aria-hidden="true" /> Rename</a>
		</li>
		<li class="copy" on:click={() => dialogs.modal(FileBrowser, {old_path: path, current_file: current_file})}>
			<a href="#0">
				<i class="fa fa-copy" aria-hidden="true" /> Copy/Move to
			</a>
		</li>
		<li class="download">
			<a on:click={downloadfile}><i class="fa fa-download" aria-hidden="true" /> Download</a>
		</li>
		<li
			class="trash"
			on:click={remove}
		>
			<a href="#"><i class="fa fa-trash" aria-hidden="true" /> Delete</a>
		</li>
	</ul>
</div>
