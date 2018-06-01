<template>
        <div class="input-group input-icon right">
            <span class= "input-group-addon">
                <i :class="txt_item.Icon"></i>
            </span>
            <input type="text" v-model="txt_item.FieldValue" :placeholder="txt_item.Placeholder" class="form-control1 icon"/>
        </div>
</template>