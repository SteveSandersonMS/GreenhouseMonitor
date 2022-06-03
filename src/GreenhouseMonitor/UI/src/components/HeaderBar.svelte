<script>
    export let greenhouse;
</script>

<nav>
    <div class="content">
        <h1>{greenhouse.name || '...'}</h1>
    </div>
</nav>

<style>
    nav {
        background: #1c5a45;
        color: white;
        height: 3rem;
        display: flex;
        align-items: center;
        position: sticky;
        top: 0;
        box-shadow: 0 1px 2px rgba(0,0,0,0.3);
        margin-bottom: 2rem;
        z-index: 1;
    }
    
    h1::before {
        content: '☰';
        margin-right: 1rem;
    }
</style>
