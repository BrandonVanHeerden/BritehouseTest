import React, { useEffect, useState } from 'react';
import articleService from '../services/articleService';
import { useAuth } from '../hooks/useAuth';
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
  const { user } = useAuth();

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
  const closeArticle = () => {
    setIsEditing(false);
    setFormState(null);
    setSelectedArticle(null);
  };

  // Helper: check if current user can manage articles (Admin or Author)
  const hasManageRole = () => {
    if (!user?.roles) return false;
    return user.roles.includes('Admin') || user.roles.includes('Author');
  };

  const isOwner = (article) => {
    const authorId = article?.authorId || article?.authorUserId || article?.userId || article?.author?.id;
    return user?.userId && authorId && user.userId === authorId;
  };

  const [isEditing, setIsEditing] = useState(false);
  const [formState, setFormState] = useState(null);

  const startCreate = () => {
    setFormState({ title: '', summary: '', content: '' });
    setIsEditing(true);
    setSelectedArticle(null);
  };

  const startEdit = (article) => {
    setFormState({ ...article });
    setIsEditing(true);
    setSelectedArticle(article);
  };

  const handleFormChange = (e) => {
    const { name, value } = e.target;
    setFormState((s) => ({ ...s, [name]: value }));
  };

  const submitForm = async (e) => {
    e.preventDefault();
    try {
      if (selectedArticle && selectedArticle.id) {
        const contract = {
          Title: formState.title,
          Summary: formState.summary,
          Content: formState.content,
          EndDate: formState.endDate ?? null,
        };

        await articleService.updateArticle(selectedArticle.id, contract);
      } else {
        await articleService.createArticle(formState);
      }
      await load();
      setIsEditing(false);
      setFormState(null);
      setSelectedArticle(null);
    } catch (err) {
      setError(err.message || 'Error saving article');
    }
  };

  const handleDelete = async (article) => {
    const ok = window.confirm('Are you sure you want to delete this article?');
    if (!ok) return;
    try {
      await articleService.deleteArticle(article.id);
      await load();
      setSelectedArticle(null);
    } catch (err) {
      setError(err.message || 'Error deleting article');
    }
  };

  // Helper to truncate long summaries to a fixed number of words
  const truncateWords = (text, wordLimit = 30) => {
    if (!text) return '';
    const words = text.split(/\s+/);
    if (words.length <= wordLimit) return text;
    return words.slice(0, wordLimit).join(' ') + '...';
  };

  // Generate a compact pagination range centered on current page
  const getPageRange = (current, total, maxButtons = 5) => {
    const half = Math.floor(maxButtons / 2);
    let start = Math.max(1, current - half);
    let end = Math.min(total, start + maxButtons - 1);
    if (end - start + 1 < maxButtons) {
      start = Math.max(1, end - maxButtons + 1);
    }
    const range = [];
    for (let i = start; i <= end; i++) range.push(i);
    return range;
  };

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

      {hasManageRole() && (
        <div className="articles-actions">
          <button onClick={startCreate}>Add Article</button>
        </div>
      )}

      {loading && <div className="articles-loading">Loading...</div>}
      {error && <div className="articles-error">{error}</div>}

      <div className="articles-table-wrap">
        <table className="articles-table">
          <thead>
            <tr>
              <th>Title</th>
              <th>Summary</th>
              <th>Author</th>
              <th>Date</th>
            </tr>
          </thead>
          <tbody>
            {items.map((a) => (
              <tr key={a.id} className="article-row" onClick={() => openArticle(a)} role="button" tabIndex={0}>
                <td className="article-title-cell">{a.title}</td>
                <td className="article-summary-cell">{truncateWords(a.summary, 28)}</td>
                <td className="article-author-cell">{a.authorName}</td>
                <td className="article-date-cell">{new Date(a.publishedDate).toLocaleString()}</td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>

      <div className="articles-pagination">
        <button className="page-btn" disabled={pageNumber <= 1} onClick={() => setPageNumber((p) => Math.max(1, p - 1))}>
          Prev
        </button>

        <span className="page-info">Page {pageNumber} of {totalPages}</span>

        <button className="page-btn" disabled={pageNumber >= totalPages} onClick={() => setPageNumber((p) => Math.min(totalPages, p + 1))}>
          Next
        </button>
      </div>

      {(selectedArticle || isEditing) && (
        <div className="modal-overlay" onClick={closeArticle}>
          <div className="modal-content" onClick={(e) => e.stopPropagation()}>
            <button className="modal-close" onClick={closeArticle} aria-label="Close">×</button>

            {isEditing ? (
              <form onSubmit={submitForm} className="article-form">
                <h2>{selectedArticle ? 'Edit Article' : 'Create Article'}</h2>
                <div className="form-group">
                  <label>Title</label>
                  <input name="title" value={formState?.title || ''} onChange={handleFormChange} required />
                </div>
                <div className="form-group">
                  <label>Summary</label>
                  <textarea name="summary" value={formState?.summary || ''} onChange={handleFormChange} />
                </div>
                <div className="form-group">
                  <label>Content</label>
                  <textarea name="content" value={formState?.content || ''} onChange={handleFormChange} />
                </div>
                <div className="form-actions">
                  <button type="submit">Save</button>
                  <button type="button" onClick={closeArticle}>Cancel</button>
                </div>
              </form>
            ) : (
              <>
                <h2 className="modal-title">{selectedArticle.title}</h2>
                <div className="modal-meta">
                  <span className="modal-author">{selectedArticle.authorName}</span>
                  <span className="modal-date">{new Date(selectedArticle.publishedDate).toLocaleString()}</span>
                </div>
                <p className="modal-summary">{selectedArticle.summary}</p>
                {selectedArticle.content && (
                  <div className="modal-content-body" dangerouslySetInnerHTML={{ __html: selectedArticle.content }} />
                )}
                {hasManageRole() && (
                  <div className="modal-actions">
                    {user?.roles?.includes('Admin') || isOwner(selectedArticle) ? (
                      <>
                        <button onClick={() => startEdit(selectedArticle)}>Edit</button>
                        <button onClick={() => handleDelete(selectedArticle)}>Delete</button>
                      </>
                    ) : null}
                  </div>
                )}
              </>
            )}

          </div>
        </div>
      )}
    </div>
  );
}

export default Articles;
