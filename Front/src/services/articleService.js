import axiosInstance from './axiosInstance';

const listArticles = async ({ pageNumber = 1, pageSize = 10, searchTerm = null, onlyActive = true } = {}) => {
  try {
    const response = await axiosInstance.get('/Article/list', {
      params: {
        pageNumber,
        pageSize,
        searchTerm,
        onlyActive,
      },
    });

    const data = response.data;
    if (data?.isSuccess) {
      return data.value;
    }

    const message = data?.error?.message || 'Failed to load articles';
    throw new Error(message);
  } catch (err) {
    throw err;
  }
};

const createArticle = async (article) => {
  try {
    const response = await axiosInstance.post('/Article/admin/create', article);
    const data = response.data;
    if (data?.isSuccess) return data.value;
    throw new Error(data?.error?.message || 'Failed to create article');
  } catch (err) {
    throw err;
  }
};

const updateArticle = async (id, contract) => {
  try {
    const response = await axiosInstance.put(`/Article/admin/update/${id}`, contract);
    const data = response.data;
    if (data?.isSuccess) return data.value;
    throw new Error(data?.error?.message || 'Failed to update article');
  } catch (err) {
    throw err;
  }
};

const deleteArticle = async (id) => {
  try {
    const response = await axiosInstance.delete('/Article/admin/delete', { params: { id } });
    const data = response.data;
    if (data?.isSuccess) return data.value;
    throw new Error(data?.error?.message || 'Failed to delete article');
  } catch (err) {
    throw err;
  }
};

export default { listArticles, createArticle, updateArticle, deleteArticle };
