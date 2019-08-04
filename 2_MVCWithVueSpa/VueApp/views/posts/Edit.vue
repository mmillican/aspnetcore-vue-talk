<template>
    <div id="edit-post">
        <h2>Edit Post</h2>
        <div class="row">
            <div class="col-md-8">
                <form v-on:submit.prevent="savePost" v-if="post">
                  <div class="form-group row">
                      <label for="title" class="col-md-3 col-form-label">Title</label>
                      <div class="col-md-9">
                          <input type="text" id="title" v-model="post.title" class="form-control" placeholder="Post title" />
                      </div>
                  </div>
                  <div class="form-group row">
                      <label for="slug" class="col-md-3 col-form-label">Slug</label>
                      <div class="col-md-9">
                          <input type="text" id="slug" v-model="post.slug" class="form-control" placeholder="post-title" />
                      </div>
                  </div>
                  <div class="form-group row">
                      <label for="post-date" class="col-md-3 col-form-label">Post Date</label>
                      <div class="col-md-9">
                          <datepicker id="postDate" v-model="post.postDate" input-class="form-control" format="MM/dd/yyyy"></datepicker>
                      </div>
                  </div>
                  <div class="form-group row">
                      <label for="content" class="col-md-3 col-form-label">Content</label>
                      <div class="col-md-9">
                          <textarea id="content" v-model="post.content" class="form-control" rows="10"></textarea>
                      </div>
                  </div>
                  <div class="form-group row">
                      <div class="col-md-12">
                          <button type="submit" class="btn btn-primary">Save</button>
                      </div>
                  </div>
                </form>
            </div>
        </div>
    </div>
</template>

<script>
import Datepicker from 'vuejs-datepicker';

export default {
  components: {
    Datepicker
  },
  data() {
    return {
      post: { }
    }
  },
  created() {
    this.getPost();
  },
  watch: {
    'id': 'getPost'
  },
  methods: {
    getPost() {
      var postId = this.$route.params.id;
      this.$http.get('/posts/id/' + postId).then(response => {
        this.post = response.data;
      })
    },
    savePost() {
      this.$http.put('/posts/' + this.post.id, this.post).then(response => {
        if (response.status === 200) {
          this.$router.push({name: 'post-list'});
        }
      })
    }
  }
}
</script>
