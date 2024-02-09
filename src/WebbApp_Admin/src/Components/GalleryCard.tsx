import React from "react";

interface GalleryCardProps {
  image_src: string;
  rootClassName: string;
}

const GalleryCard3: React.FC<GalleryCardProps> = ({ image_src, rootClassName }) => {
  return (
    <div className={rootClassName}>
      <img src={image_src} alt="Gallery Card" className="gallery-card-image" />
    </div>
  );
};

export default GalleryCard3;