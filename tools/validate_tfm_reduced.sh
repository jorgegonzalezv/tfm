#!/bin/bash

# validate trained yolov5 on tfm dataset over SeaDroneSea validation dataset.
LIBPATH=lib/yolov5-master
MODELPATH=$LIBPATH/runs/train/19_07_tfm_v3_unfrozen/weights/best.pt

# seadronesee
python $LIBPATH/val.py \
    --data datasets/seadronesee_val_1/data_tfm.yaml \
    --name 19_07_tfm_v3_unfrozen_sds_val_1 \
    --weights $MODELPATH \
    --save-txt \
    --save-json \
    --save-conf \
    --iou-thres 0.2

    #--conf-thres 0.5  # checkpoint to trained model

# tfm dataset validation 
# python $LIBPATH/val.py --data datasets/tfm/data.yaml --name tfm_17_06_tfm_resume --weights $LIBPATH/runs/train/17_06_tfm_resume/weights/best.pt # --conf-thres 0.5  # checkpoint to trained model
# python $LIBPATH/val.py --data datasets/tfm/data_coco.yaml --name tfm_17_06_coco_pretrained --weights $LIBPATH/yolov5s.pt # --conf-thres 0.5  # checkpoint to trained model
