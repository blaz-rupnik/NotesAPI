<template>
    <div>
        <div class="my-folders-header">
            <h1>My folders</h1>
            <!-- <button class="btn add-button">
                <span class="fa fa-plus"></span>
                <p>Add</p>
            </button> -->
        </div>
        <div id="my-folders">
            <div class="my-folder" v-for="folder in this.folders" v-bind:key="folder.id">
                <span class="fa fa-folder-o folder-icon"></span>
                <div @click="openFolder(folder.id)" class="folder-name">{{ folder.name }}</div>
                <span @click="deleteFolder(folder.id)" class="fa fa-trash delete-icon"></span>
            </div>
        </div>
    </div>
</template>

<script>
import { folderService } from '../services/folder.service';

export default {
    data() { 
        return {
            folders: ''
        };
    },
    created() {
        folderService.getAll()
            .then(
                folders => this.folders = folders,
                error => console.log(error)
            )
    },
    methods: {
        openFolder(id){
            console.log("TODO: Open folder with notes");
        },
        deleteFolder(id){
            folderService.deleteFolder(id)
                .then(
                    result => {
                        let index = this.folders.findIndex(x => x.id === id);
                        this.folders.splice(index, 1);
                    },
                    error => console.log(error)
                )
        }
    }
}
</script>
<style scoped>
#my-folders {
    display: flex;
    flex-direction: row;
    flex-wrap: wrap;
    margin-top: 10px;
}
.my-folders-header {
    display: flex;
    justify-content: start;
}
.my-folder {
    margin: 5px;
    width: 300px;
    display: flex;  
}

.delete-icon {
    color: #dc3545;
    cursor: pointer;
    margin-top: 7px;
}
.folder-icon {
    font-size: 48px;
    margin: 5px;
}
.add-button {
    display: inline;
}
.folder-name {
    margin: auto 3px;
    font-size: 20px;
    cursor: pointer;
}
</style>