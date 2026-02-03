import React, { useEffect, useState } from 'react';
import articleService from '../services/articleService';
import './Articles.css';

function Articles() {
  const [items, setItems] = useState([]);
  const [pageNumber, setPageNumber] = useState(1);
  const [pageSize] = useState(10);
  const [totalPages, setTotalPages] = useState(1);
  const [searchTerm, setSearchTerm] = useState('');
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);
  const [selectedArticle, setSelectedArticle] = useState(null);

  const load = async () => {
    setLoading(true);
    setError(null);
    try {
      const data = await articleService.listArticles({ pageNumber, pageSize, searchTerm: searchTerm || null, onlyActive: true });
      setItems(data.items || []);
      setTotalPages(data.totalPages || 1);
    } catch (err) {
      setError(err.message || 'Error loading articles');
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    load();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [pageNumber]);

  const handleSearch = async (e) => {
    e.preventDefault();
    setPageNumber(1);
    await load();
  };

  useEffect(() => {
    const onKey = (e) => {
      if (e.key === 'Escape') setSelectedArticle(null);
    };
    window.addEventListener('keydown', onKey);
    return () => window.removeEventListener('keydown', onKey);
  }, []);

  const openArticle = (article) => setSelectedArticle(article);
  const closeArticle = () => setSelectedArticle(null);

  return (
    <div className="articles-page">
      <h1>Articles</h1>

      <form className="articles-search" onSubmit={handleSearch}>
        <input
          type="text"
          placeholder="Search articles..."
          value={searchTerm}
          onChange={(e) => setSearchTerm(e.target.value)}
        />
        <button type="submit">Search</button>
      </form>

      {loading && <div className="articles-loading">Loading...</div>}
      {error && <div className="articles-error">{error}</div>}

      <ul className="articles-list">
        {items.map((a) => (
          <li key={a.id} className="article-item" onClick={() => openArticle(a)} role="button" tabIndex={0}>
            <h3 className="article-title">{a.title}</h3>
            <p className="article-summary">{a.summary}</p>
            <div className="article-meta">
              <span className="article-author">{a.authorName}</span>
              <span className="article-date">{new Date(a.publishedDate).toLocaleString()}</span>
            </div>
          </li>
        ))}
      </ul>

      <div className="articles-pagination">
        <button disabled={pageNumber <= 1} onClick={() => setPageNumber((p) => Math.max(1, p - 1))}>
          Prev
        </button>
        <span>
          Page {pageNumber} of {totalPages}
        </span>
        <button disabled={pageNumber >= totalPages} onClick={() => setPageNumber((p) => Math.min(totalPages, p + 1))}>
          Next
        </button>
      </div>

      {selectedArticle && (
        <div className="modal-overlay" onClick={closeArticle}>
          <div className="modal-content" onClick={(e) => e.stopPropagation()}>
            <button className="modal-close" onClick={closeArticle} aria-label="Close">×</button>
            <h2 className="modal-title">{selectedArticle.title}</h2>
            <div className="modal-meta">
              <span className="modal-author">{selectedArticle.authorName}</span>
              <span className="modal-date">{new Date(selectedArticle.publishedDate).toLocaleString()}</span>
            </div>
            <p className="modal-summary">{selectedArticle.summary}</p>
          </div>
        </div>
      )}
    </div>
  );
}

export default Articles;
