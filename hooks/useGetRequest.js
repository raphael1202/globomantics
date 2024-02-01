import loadingStatus from '@/helpers/loadingStatus';
import { useCallback, useState } from 'react';

const useGetRequest = (url) => {
  const [loadingState, setLoadingState] = useState(loadingStatus.isLoading);

  const get = useCallback(async () => {
    setLoadingState(loadingStatus.isLoading);

    try {
      const response = await fetch(url);
      const result = await response.json();
      setLoadingState(loadingStatus.loaded);

      return result;
    } catch {
      setLoadingState(loadingStatus.hasErrored);
    }
  }, [url]);

  return { get, loadingState };
};

export default useGetRequest;
