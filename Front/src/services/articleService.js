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

export default { listArticles };
