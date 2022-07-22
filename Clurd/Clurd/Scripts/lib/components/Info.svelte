<script>
    import { DialogContent, getClose} from "svelte-dialogs";
    export let file, path, size, lastaccess, lastwrite, creation, readonly;
    const close = getClose();
    </script>


<DialogContent>
    <h1 id={file} slot="header">{file}</h1>
    <svelte:fragment slot="body">
      <ul>
        <li>Path: {path}</li>
        <li>Last Access: {lastaccess}</li>
        <li>Last Write: {lastwrite}</li>
        <li>Creation: {creation}</li>
        <li>Size: {size}</li>
        <li>Read-Only: {readonly}</li>
      </ul>
    </svelte:fragment>
    <svelte:fragment slot="footer">
      <button on:click={() => close()} class="btn btn-primary">Close</button>
    </svelte:fragment>
  </DialogContent>
  