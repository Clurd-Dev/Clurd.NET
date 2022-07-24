<script>
    import { onMount } from 'svelte';
    import { getinfo } from '$lib/js/systeminfo.js';
    import { Circle2 } from 'svelte-loading-spinners';
    import 'bootstrap/dist/css/bootstrap.css';
    let information, loading = true;
    onMount(async() => {
        information = await getinfo();
        console.log(information);
        loading = false;
    });
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
</div>
{/if}
<style>
    .info{
        margin: 20px;
    }
</style>
