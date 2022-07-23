<script>
	export let current_file, path, pathsplitted;
	let url;
	import { dialogs } from 'svelte-dialogs';
	// import { copy } from '../ts/copy';
	import { onMount } from 'svelte';
	// import FSBrowser from '../file-browser/browser.svelte';
	// import Details from '$lib/details/details.svelte';
	import { Rename } from '$lib/js/io.js';
    import Fa from 'svelte-fa/src/fa.svelte';
    import { faCopy } from '@fortawesome/free-solid-svg-icons/index.es';
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
</script>

<svelte:head>
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
		<!-- <li class="link">
			<a href="#0" on:click={copy(current_file)}
				><i class="fa fa-link" aria-hidden="true" /> Copy Link Address</a
			>
		</li>
		<li class="copy">
			<a
				href="#0"
				on:click={() =>
					dialogs.modal(FSBrowser, {
						ls: ls,
						path: path,
						current_name: current_name,
						current_file: current_file,
						file: only_file,
						old_path: old_path
					})}
			>
				<i class="fa fa-copy" aria-hidden="true" /> Copy/Move to
			</a>
		</li>
		<li class="download">
			<a href={current_file}><i class="fa fa-download" aria-hidden="true" /> Download</a>
		</li>
		<li
			class="trash"
			on:click={remove(current_file.replace(url, ''), getfile, path, only_file)}
		>
			<a href="#"><i class="fa fa-trash" aria-hidden="true" /> Delete</a>
		</li>
		<li
			class="trash"
			on:click={() =>
				dialogs.modal(Details, {
					ls: ls,
					name_file: current_file,
					path: path,
					current_name: current_name,
					file_name: only_file
				})}
		>
			<a href="#"><i class="fa fa-trash" aria-hidden="true" /> Details</a>
		</li> -->
	</ul>
</div>
